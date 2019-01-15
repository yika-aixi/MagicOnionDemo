[例子来源]("https://qiita.com/mitchydeath/items/cecf01493d1efeb4ae55")

[框架库](https://github.com/Cysharp/MagicOnion)

[框架Unity支持说明](https://github.com/cysharp/MagicOnion#unity-supports)

这个例子里个人在架构架构这块和作者中的不一样,说明下

- 首先服务器代码在`MagicOnionTestService`文件夹

- *服务器中引用了`Assets\Test\Interfaces\`路径下的`IChat.cs`和`IChatHub.cs`,例子中是客户端复制了这2个接口的代码,我是服务端引用了,为什么在服务端,是因为客户端那边Unity会修改`.csproj`所以放到了服务端

