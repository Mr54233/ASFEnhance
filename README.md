# ASFEnhance

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/3d174e792fd4412bb6b34a77d67e5dea)](https://www.codacy.com/gh/chr233/ASFEnhance/dashboard)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/chr233/ASFEnhance/AutoBuild?logo=github)
[![GitHub Release](https://img.shields.io/github/v/release/chr233/ASFEnhance?logo=github)](https://github.com/chr233/ASFEnhance/releases)
[![GitHub Download](https://img.shields.io/github/downloads/chr233/ASFEnhance/total?logo=github)](https://img.shields.io/github/v/release/chr233/ASFEnhance)
![GitHub last commit](https://img.shields.io/github/last-commit/chr233/ASFEnhance?logo=github)

[![License](https://img.shields.io/github/license/chr233/ASFEnhance?logo=apache)](https://github.com/chr233/ASFEnhance/blob/master/license)
![GitHub Repo stars](https://img.shields.io/github/stars/chr233/ASFEnhance?logo=github)
[![Gitee Stars](https://gitee.com/chr_a1/ASFEnhance/badge/star.svg?theme=dark)](https://gitee.com/chr_a1/ASFEnhance/stargazers)

[![爱发电](https://img.shields.io/badge/爱发电-chr__-ea4aaa.svg?logo=github-sponsors)](https://afdian.net/@chr233)
[![Bilibili](https://img.shields.io/badge/bilibili-Chr__-00A2D8.svg?logo=bilibili)](https://space.bilibili.com/5805394)
[![Steam](https://img.shields.io/badge/steam-Chr__-1B2838.svg?logo=steam)](https://steamcommunity.com/id/Chr_)

[中文说明](README.zh-CN.md)

---

[![Repobeats analytics image](https://repobeats.axiom.co/api/embed/df6309642cc2a447195c816473e7e54e8ae849f9.svg "Repobeats analytics image")](https://github.com/chr233/ASFEnhance/pulse)

---

## Download

> Unzip the ASFEnhance.dll and copy it into the "plugins" folder in the ASF's directory to install

[码云镜像 (国内推荐)](https://gitee.com/chr_a1/ASFEnhanceRelease/tree/master/%E6%9C%80%E6%96%B0%E7%89%88%E6%9C%AC)

[GitHub Releases](https://github.com/chr233/ASFEnhance/releases)

## Support Version

> The \* mark means the ASFEnhance is compatibility with the ASF in theory, but haven't tested.

| ASFEnhance Version                                                         | Depended ASF | 5.1.2.5 | 5.2.2.5 | 5.2.3.7 | 5.2.4.2 |
| -------------------------------------------------------------------------- | ------------ | ------- | ------- | ------- | ------- |
| [1.5.17.289](https://github.com/chr233/ASFEnhance/releases/tag/1.5.17.289) | 5.2.4.2      | ❌      | ✔️\*    | ✔️\*    | ✔️      |
| [1.5.16.260](https://github.com/chr233/ASFEnhance/releases/tag/1.5.16.260) | 5.2.4.2      | ❌      | ✔️\*    | ✔️\*    | ✔️      |
| [1.5.15.257](https://github.com/chr233/ASFEnhance/releases/tag/1.5.15.257) | 5.2.3.7      | ❌      | ✔️\*    | ✔️      | ✔️      |
| [1.5.14.235](https://github.com/chr233/ASFEnhance/releases/tag/1.5.14.235) | 5.2.2.5      | ❌      | ✔️      | ✔️      | ✔️\*    |
| [1.5.13.231](https://github.com/chr233/ASFEnhance/releases/tag/1.5.13.231) | 5.1.2.5      | ✔️      | ❌      | ❌      | ❌      |
| [1.5.12.230](https://github.com/chr233/ASFEnhance/releases/tag/1.5.12.230) | 5.1.2.5      | ✔️      | ❌      | ❌      | ❌      |

## TODO

- [x] Fix `JOINGROUP` command
- [x] Add commands list joined groups and leave specified groups
- [ ] Fix `SETCOUNTRY` command
- [ ] Add command to send text message
- [ ] Add command to craft booster packs
- [ ] Add command to clear all notice

## New Commands

### Common Commands

| Command      | Shorthand | Access | Description                       |
| ------------ | --------- | ------ | --------------------------------- |
| `KEY <Text>` | `K`       | `Any`  | Extract keys from plain text      |
| `ASFENHANCE` | `ASFE`    | `Any`  | Get the version of the ASFEnhance |

## Community Commands

| Command                       | Shorthand | Access          | Description                      |
| ----------------------------- | --------- | --------------- | -------------------------------- |
| `PROFILE [Bots]`              | `PF`      | `FamilySharing` | Get bot's profile infomation     |
| `STEAMID [Bots]`              | `SID`     | `FamilySharing` | Get bot's steamID                |
| `FRIENDCODE [Bots]`           | `FC`      | `FamilySharing` | Get bot's friend code            |
| `GROUPLIST [Bots]`            | `GL`      | `FamilySharing` | Get bot's joined group list      |
| `JOINGROUP [Bots] <GroupUrl>` | `JG`      | `Master`        | Let bot to join specified group  |
| `LEAVEGROUP [Bots] <GroupID>` | `LG`      | `Master`        | Let bot to leave specified group |

> `GroupID` can be found using `GROUPLIST` command

### Wishlist Commands

| Command                          | Shorthand | Access   | Description                     |
| -------------------------------- | --------- | -------- | ------------------------------- |
| `ADDWISHLIST [Bots] <AppIDs>`    | `AW`      | `Master` | Add game to bot's wishlist      |
| `REMOVEWISHLIST [Bots] <AppIDs>` | `RW`      | `Master` | Delete game from bot's wishlist |

### Store Commands

| Command                                    | Shorthand | Access     | Description                                                                         |
| ------------------------------------------ | --------- | ---------- | ----------------------------------------------------------------------------------- |
| `APPDETAIL [Bots] <AppIDS>`                | `AD`      | `Operator` | Get app detail from steam API, support `APP`                                        |
| `SUBS [Bots] <AppIDS\|SubIDS\|BundleIDS>`  | `S`       | `Operator` | Get available subs from store page, support `APP/SUB/BUNDLE`                        |
| `PUBLISHRECOMMENT [Bots] <AppIDS> COMMENT` | `PREC`    | `Operator` | Publish a recomment for game, if appID > 0 it will rateUp, or if appId < 0 rateDown |
| `DELETERECOMMENT [Bots] <AppIDS>`          | `DREC`    | `Operator` | Delete a recomment for game (Still have BUG, not working yet)                       |

### Cart Commands

> Steam saves cart information via cookies, restart bot instance will let shopping cart being emptied

| Command                              | Shorthand | Access     | Description                                                                    |
| ------------------------------------ | --------- | ---------- | ------------------------------------------------------------------------------ |
| `CART [Bots]`                        | `C`       | `Operator` | Get bot's cart information                                                     |
| `ADDCART [Bots] <SubIDs\|BundleIDs>` | `AC`      | `Operator` | Add game to bot's cart, only support `SUB/BUNDLE`                              |
| `CARTRESET [Bots]`                   | `CR`      | `Operator` | Clear bot's cart                                                               |
| `CARTCOUNTRY [Bots]`                 | `CC`      | `Operator` | Get bot's available currency area (Depends to wallet area and the IP location) |
| `SETCOUNTRY [Bots] <CountryCode>`    | `SC`      | `Operator` | Set bot's currency area (NOT WORKING, WIP)                                     |
| `PURCHASE [Bots]`                    | `PC`      | `Master`   | Purchase bot's cart items for it self (paid via steam wallet)                  |
| `PURCHASEGIFT [BotA] BotB`           | `PCG`     | `Master`   | Purchase botA's cart items for botB as gift (paid via steam wallet)            |

> Steam allows duplicate purchases, please check cart before using PURCHASE command.

## Shorthand Commands

| Shorthand              | Equivalent Command             | Description                    |
| ---------------------- | ------------------------------ | ------------------------------ |
| `AL [Bots] <Licenses>` | `ADDLICENSE [Bots] <Licenses>` | Add free `SUB`                 |
| `LA`                   | `LEVEL ASF`                    | Get All bot's level            |
| `BA`                   | `BALANCE ASF`                  | Get All bot's wallet balance   |
| `PA`                   | `POINTS ASF`                   | Get All bot's points balance   |
| `P [Bots]`             | `POINTS`                       | Get bot's points balance       |
| `CA`                   | `CART ASF`                     | Get All bot's cart information |

## For Developer

> This group of commands is disabled by default.
> You need to add `"ASFEnhanceDevFuture": true` in `ASF.json` to enable it.

| 命令                 | 权限     | 说明                      |
| -------------------- | -------- | ------------------------- |
| `COOKIES [Bots]`     | `Master` | 查看 Steam 商店的 Cookies |
| `APIKEY [Bots]`      | `Master` | 查看 Bot 的 APIKey        |
| `ACCESSTOKEN [Bots]` | `Master` | 查看 Bot 的 ACCESSTOKEN   |

## Download Link

[Releases](https://github.com/chr233/ASFEnhance/releases)

[workflow_b]: https://github.com/chr233/ASFEnhance/actions/workflows/dotnet.yml/badge.svg
[workflow]: https://github.com/chr233/ASFEnhance/actions/workflows/dotnet.yml
[codacy_b]: https://app.codacy.com/project/badge/Grade/3d174e792fd4412bb6b34a77d67e5dea
[codacy]: https://www.codacy.com/gh/chr233/ASFEnhance/dashboard
[download_b]: https://img.shields.io/github/downloads/chr233/ASFEnhance/total
[release]: https://github.com/chr233/ASFEnhance/releases
[release_b]: https://img.shields.io/github/v/release/chr233/ASFEnhance
[license]: https://github.com/chr233/ASFEnhance/blob/master/license
[license_b]: https://img.shields.io/github/license/chr233/ASFEnhance
