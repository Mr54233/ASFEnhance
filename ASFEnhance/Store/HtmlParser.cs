﻿#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。

using AngleSharp.Dom;
using AngleSharp.XPath;
using ArchiSteamFarm.Core;
using ArchiSteamFarm.Web.Responses;
using ASFEnhance.Data;
using ASFEnhance.Localization;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using static ASFEnhance.Store.CurrencyHelper;
using static ASFEnhance.Store.Response;
using static ASFEnhance.Utils;

namespace ASFEnhance.Store
{
    internal static class HtmlParser
    {
        /// <summary>
        /// 解析商店页面
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static GameStorePageResponse? ParseStorePage(HtmlDocumentResponse response)
        {
            if (response == null)
            {
                return null;
            }

            IEnumerable<IElement> gameNodes = response.Content.SelectNodes("//div[@id='game_area_purchase']/div[contains(@class,'purchase')]");

            HashSet<SingleSubData> subInfos = new();

            foreach (IElement gameNode in gameNodes)
            {
                IElement? eleName = gameNode.SelectSingleElementNode(".//h1");
                IElement? eleForm = gameNode.SelectSingleElementNode(".//form");
                IElement? elePrice = gameNode.SelectSingleElementNode(".//div[@data-price-final]");

                if (eleName == null)
                {
                    ASFLogger.LogGenericDebug(string.Format(Langs.SomethingIsNull, nameof(eleName)));
                    continue;
                }

                string subName = eleName?.Text() ?? string.Format(Langs.GetStoreNameFailed);

                subName = Regex.Replace(subName, @"\s+|\(\?\)", " ").Trim();

                if (eleForm != null && elePrice != null) // 非免费游戏
                {
                    string finalPrice = elePrice.GetAttribute("data-price-final") ?? "0";
                    string formName = eleForm.GetAttribute("name") ?? "-1";
                    Match match = Regex.Match(formName, @"\d+$");

                    uint subID = 0, price = 0;

                    if (match.Success)
                    {
                        if (!uint.TryParse(match.Value, out subID) || !uint.TryParse(finalPrice, out price))
                        {
                            ASFLogger.LogGenericWarning(string.Format("{0} or {1} cant parse to uint", nameof(formName), nameof(finalPrice)));
                        }
                    }

                    bool isBundle = formName.Contains("bundle");

                    subInfos.Add(new(isBundle, subID, subName, price));
                }
            }
            IElement? eleGameName = response.Content.SelectSingleNode("//div[@id='appHubAppName']|//div[@class='page_title_area game_title_area']/h2");
            string gameName = eleGameName?.Text() ?? string.Format(Langs.GetStoreNameFailed);

            if (subInfos.Count == 0)
            {
                IElement? eleError = response.Content.SelectSingleNode("//div[@id='error_box']/span");

                gameName = eleError?.Text() ?? string.Format(Langs.StorePageNotFound);
            }

            return new GameStorePageResponse(subInfos, gameName);
        }

        /// <summary>
        /// 解析搜索页
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static string? ParseSearchPage(HtmlDocumentResponse response)
        {
            if (response == null)
            {
                return null;
            }

            IEnumerable<IElement> gameNodes = response.Content.SelectNodes("//div[@id='search_resultsRows']/a[contains(@class,'search_result_row')]");

            if (!gameNodes.Any())
            {
                return Langs.SearchResultEmpty;
            }

            StringBuilder result = new();

            result.AppendLine(Langs.MultipleLineResult);

            result.AppendLine(Langs.SearchResultTitle);

            foreach (IElement gameNode in gameNodes)
            {
                IElement? eleTitle = gameNode.SelectSingleElementNode(".//span[@class='title']");

                if (eleTitle == null)
                {
                    ASFLogger.LogGenericDebug(string.Format(Langs.SomethingIsNull, nameof(eleTitle)));
                    continue;
                }

                string gameTitle = eleTitle.Text();

                string gameHref = gameNode.GetAttribute("href");

                Match match = Regex.Match(gameHref, @"((app|sub|bundle)\/\d+)");

                if (match.Success)
                {
                    result.AppendLine(string.Format(Langs.AreaItem, match.Value, gameTitle));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// 获取Cursor对象
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static CursorData? ParseCursorData(HtmlDocumentResponse response)
        {
            if (response == null)
            {
                return null;
            }

            string content = response.Content.Body.InnerHtml;
            Match match = Regex.Match(content, @"g_historyCursor = ([^;]+)");
            if (!match.Success)
            {
                return null;
            }

            content = match.Groups[1].Value;
            try
            {
                CursorData? cursorData = JsonConvert.DeserializeObject<CursorData>(content);
                return cursorData;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 解析历史记录条目
        /// </summary>
        /// <param name="tableElement"></param>
        /// <param name="currencyRates"></param>
        /// <param name="defaultCurrency"></param>
        /// <returns></returns>
        internal static HistoryParseResponse ParseHistory(IElement tableElement, Dictionary<string, double> currencyRates, string defaultCurrency)
        {
            Regex pattern = new(@"^\s*([-+])?([^\d,.]*)([\d,.]+)([^\d,.]*)$");

            // 识别货币符号
            double ParseSymbol(string symbol1, string symbol2)
            {
                const char USD = '$';
                const char RMB = '¥';

                string currency = string.Empty;

                if (!string.IsNullOrEmpty(symbol1))
                {
                    if (SymbolCurrency.ContainsKey(symbol1))
                    {
                        currency = SymbolCurrency[symbol1];
                    }
                }

                if (!string.IsNullOrEmpty(symbol2))
                {
                    if (SymbolCurrency.ContainsKey(symbol2))
                    {
                        currency = SymbolCurrency[symbol2];
                    }
                }

                if (string.IsNullOrEmpty(currency))
                {
                    if (symbol1.Contains(USD) || symbol2.Contains(USD))
                    {
                        currency = "USD";
                    }
                    else if (symbol1.Contains(RMB) || symbol2.Contains(RMB))
                    {
                        currency = defaultCurrency;
                    }
                    else
                    {
                        currency = "";
                    }
                }

                if (!string.IsNullOrEmpty(currency) && currencyRates.ContainsKey(currency))
                {
                    return currencyRates[currency];
                }
                else
                {
                    return 1;
                }
            }

            // 识别货币数值
            int ParseMoneyString(string strMoney)
            {
                Match match = pattern.Match(strMoney);

                if (!match.Success)
                {
                    return 0;
                }
                else
                {
                    bool negative = match.Groups[1].Value == "-";
                    string symbol1 = match.Groups[2].Value.Trim();
                    string strPrice = match.Groups[3].Value.Replace(",", "").Replace(".", "");
                    string symbol2 = match.Groups[4].Value.Trim();

                    if (!int.TryParse(strPrice, out int price))
                    {
                        return 0;
                    }

                    double rate = ParseSymbol(symbol1, symbol2);

                    return (negative ? -1 : 1) * (int)(price / rate);
                }
            }

            HistoryParseResponse result = new();

            IHtmlCollection<IElement> rows = tableElement.QuerySelectorAll("tr");

            foreach (IElement row in rows)
            {
                if (!row.HasChildNodes)
                {
                    continue;
                }

                IElement whtItem = row.QuerySelector("td.wht_items");
                IElement whtType = row.QuerySelector("td.wht_type");
                IElement whtTotal = row.QuerySelector("td.wht_total");
                IElement whtChange = row.QuerySelector("td.wht_wallet_change.wallet_column");

                bool isRefund = whtType.ClassName.Contains("wht_refunded");

                string strItem = whtItem?.Text().Trim().Replace("\t", "") ?? "";
                string strType = whtType?.Text().Trim().Replace("\t", "") ?? "";
                string strTotal = whtTotal?.Text().Trim().Replace("\t", "") ?? "";
                string strChange = whtChange?.Text().Trim().Replace("\t", "") ?? "";

                if (!string.IsNullOrEmpty(strType))
                {
                    // 排除退款和转换货币
                    if (!string.IsNullOrEmpty(strType) && !strType.StartsWith("转换") && !strType.StartsWith("退款"))
                    {
                        int total = ParseMoneyString(strTotal);
                        int walletChange;

                        if (string.IsNullOrEmpty(strChange))
                        {
                            walletChange = 0;
                        }
                        else
                        {
                            walletChange = Math.Abs(ParseMoneyString(strChange));
                        }

                        if (total == 0)
                        {
                            continue;
                        }

                        if (strType.StartsWith("购买"))
                        {
                            if (!strItem.Contains("钱包资金"))
                            {
                                if (!isRefund)
                                {
                                    result.StorePurchase += total;
                                    result.StorePurchaseWallet += walletChange;
                                }
                                else
                                {
                                    result.RefundPurchase += total;
                                    result.RefundPurchaseWallet += walletChange;
                                }
                            }
                            else
                            {
                                result.WalletPurchase += total;
                            }
                        }
                        else if (strType.StartsWith("礼物购买"))
                        {
                            if (!isRefund)
                            {
                                result.GiftPurchase += total;
                                result.GiftPurchaseWallet += walletChange;
                            }
                            else
                            {
                                result.RefundPurchase += total;
                                result.RefundPurchaseWallet += walletChange;
                            }
                        }
                        else if (strType.StartsWith("游戏内购买"))
                        {
                            if (!isRefund)
                            {
                                result.InGamePurchase += walletChange;
                            }
                            else
                            {
                                result.RefundPurchase += walletChange;
                                result.RefundPurchaseWallet += walletChange;
                            }
                        }
                        else if (strType.StartsWith("市场交易") || strType.Contains("市场交易"))
                        {
                            if (!isRefund)
                            {
                                if (walletChange >= 0)
                                {
                                    result.MarketSelling += total;
                                }
                                else
                                {
                                    result.MarketPurchase += total;
                                }
                            }
                            else
                            {
                                result.RefundPurchase += walletChange;
                            }
                        }
                        else
                        {
                            if (!isRefund)
                            {
                                result.Other += total;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
