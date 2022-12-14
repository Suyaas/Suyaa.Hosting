# 舒雅令牌微服务(Suyaa Token)

基于.Net 6开发的令牌微服务

An token microservice for .Net 6

![license](https://img.shields.io/github/license/SuyaaUI/Suyaa.Token)
![codeSize](https://img.shields.io/github/languages/code-size/SuyaaUI/Suyaa.Token)
![lastCommit](https://img.shields.io/github/last-commit/SuyaaUI/Suyaa.Token)

## 配置方式

通过配置apps.json文件的方式，加载令牌配置，配置方式如下：

```
{
    "apps":[
        { "app_id": "应用标识1", "app_name": "名称1", "app_key": "密钥1", app_time:"令牌有效时长1" },
        { "app_id": "应用标识2", "app_name": "名称2", "app_key": "密钥2", app_time:"令牌有效时长2" }
    ]
}
```

## 管理令牌方式

通过WebApi操作令牌，令牌与交互历史通过Sqlite数据库进行存储。

### 申请令牌

```
// 请求地址
/create_token?app_id=xxx&apply_time=123456789&app_sign=zzz
```

### 续订令牌

```
// 请求地址
/renew_token?token=xxx&apply_time=123456789&app_sign=zzz
```

## 基于时间戳的签名算法

为保证通讯的安全性，使用sha512进行签名校验

假设唯一标识(app_id)为xxx，时间戳(apply_time)为123456789，密钥(app_key)为yyy，那算法为：

```
str = "app_id=xxx&app_key=yyy&apply_time=123456789"
app_sign = sha512(str)
```


