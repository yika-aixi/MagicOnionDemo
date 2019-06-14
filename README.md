MagicOnion 学习制作的的Demo
包含内容:`房间管理`(和脑子里的那个房间管理不一样,MagicOnion)的广播他通过上下文去反射调用客户端,然后每个Hub都要连接下,否则只是从房间的hub加入然后在其他hub获取然后广播,客户端是收不到的,所以所有的hub都要连接下,这样造成,其实这个房间管理是多余的存在,完全没必要,看作者文档他似乎会在以后提供一个管理功能)`房间服务`(获取房间信息)`玩家操作同步`(移动,跳跃,蹲下,旋转)`聊天`


[例子来源](https://qiita.com/mitchydeath/items/cecf01493d1efeb4ae55)

[框架库](https://github.com/Cysharp/MagicOnion)

[框架Unity支持说明](https://github.com/cysharp/MagicOnion#unity-supports)



客户端(注意要启动服务器噢):
- Unity打开`Assets\Scenes\0.Main.unity`开始游戏就可以了,记得先运行服务器.

服务端:
- 打开`MagicOnionTestService\MagicOnionTestService.sln`运行就行
