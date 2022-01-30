# <font color = "cornflowerblue">SpiderSolitaire</font>
====
蜘蛛纸牌C#-工作台

# <font color = "cornflowerblue">开发文档</font>
## <font color = "orange">游戏规则</font>

蜘蛛纸牌简介  
- 将十叠牌中所有最前面的牌都移走
- 如要将十叠牌中所有最前面的牌都移走，请将牌从一列移到另一列，直到将一牌套从 K 到 A 依次排齐。当一组牌从 K 到 A 依次排齐时，这些牌就会被移走。

蜘蛛纸牌玩法
- 在“游戏”菜单上，单击“开局”。
- “蜘蛛纸牌”用两副牌玩。新一局游戏开始时，发有十叠牌，每叠中只有一张正面朝上。其余的牌放在窗口右下角的五叠牌叠中；新一轮发牌时用这些牌。
- 移牌的方法是将牌从一个牌叠拖到另一个牌叠。移牌的规则如下：
- 可以将牌叠最底下的牌移到空牌叠。
- 可以将牌从牌叠最底下移到牌值仅次于它的牌上，不论牌套或颜色如何。
- 可以像对待一张牌一样移动一组同样牌套、依序排好的牌。
- 准备新一轮发牌时，请单击“发牌”，或者单击窗口右下角的牌叠。
- 在新一轮发牌之前，每一叠中都必须有牌。

注意事项
- 如要以不同的难易级别开始新一局游戏，请单击“游戏”菜单上的“难易级别”，然后选择某种难易级别。
- 要看到可以执行的合法操作，请单击”游戏”菜单，然后单击“显示可行的操作”。还可单击屏幕底部的得分框。
- 如要保存游戏以便以后接着玩，请单击“游戏”菜单上的“保存本次游戏”。
- 如要更改游戏选项，请单击“游戏”菜单上的“选项”。
- 如要查看或清除游戏统计数据信息，请单击“游戏”菜单上的“统计数据”。

蜘蛛纸牌中的得分
- 每局“蜘蛛纸牌”开始时有 500 分。根据下列规则可能会加分或减分：
- 每移动一次牌丢一分。
- 每次单击“游戏”菜单中的“撤消”，都将丢一分。
- 每次将整个牌套按顺序从 K 排到 A，则该套牌将从玩牌区移走，并且您会得 100 分。


## <font color = "orange">游戏实现</font>
- 扑克牌——数组
- 移动——方法
- 判断游戏结束——方法

## <font color = "orange">开发难点</font>
- [x] 扑克牌随机初始化
- [ ] 随机赋值问题的解决（将扑克牌数组随机赋值1~13）

- 算法一
    产生随机数种子，限制赋值范围
    判断：如果扑克牌数组中一个随机数的数目等于10
    重新随机，直至出现产生合适随机数
    缺点
        最坏时间复杂度为无穷大，低效


- 算法二（采用）
    初始化扑克牌数组
    随机交换130次



移动方法
游戏结束判断
