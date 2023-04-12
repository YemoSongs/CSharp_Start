internal class Program
{
    private static void Main(string[] args)
    {
        #region 1.控制台基础设置
        //隐藏光标
        Console.CursorVisible = false;
        //通过两个变量来储存控制器窗口和缓冲区的宽高
        int w = 50;
        int h = 30;
        //更改窗口大小和缓冲区大小
        Console.SetWindowSize(w, h);
        Console.SetBufferSize(w, h);
        #endregion
        #region 2.多个场景
        //创建场景编号
        int nowSceneID =1;
        //创建一个循环，来进行场景更换
        while (true)
        {
            //用switch来更换场景
            switch (nowSceneID)
            {
                case 1:
                    #region 场景1 开始场景
                    Console.Clear();//更换场景后，清空上一个场景

                    //让标题居中
                    Console.SetCursorPosition(w/2-7, 8);
                    Console.Write("唐老师营救公主");
                    //定义选项的标号
                    int nowSelIndex = 0;
                    //定义一个标签，方便在switch中跳出外层while循环
                    bool isQuitwhile;
                    while (true)
                    {
                        
                         isQuitwhile = false;
                        
                        Console.SetCursorPosition(w / 2 - 4, 13);
                        //用三目运算符，判断是否选中该选项，来更改其文字颜色
                        Console.ForegroundColor=nowSelIndex==0?ConsoleColor.Green:ConsoleColor.White;
                        Console.Write("开始游戏");

                        Console.SetCursorPosition(w / 2 - 4, 15);
                        Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Green : ConsoleColor.White;
                        Console.Write("退出游戏");
                        //获取玩家输入，来切换选择对象
                        Char c=Console.ReadKey(true).KeyChar;
                        switch(c)
                        {
                            case 'W':
                            case 'w':
                                //当按下w时，选择相应选项
                                --nowSelIndex;
                                nowSelIndex=nowSelIndex<0?0:nowSelIndex;
                                break;
                            case 'S':
                            case 's':
                                ++nowSelIndex;
                                nowSelIndex = nowSelIndex > 1 ? 1 : nowSelIndex;
                                break;
                            case 'J':
                            case 'j':
                                //当按下j时，相当于确定，分别执行应当执行的操作
                                if (nowSelIndex == 0)
                                {
                                    //场景变为2
                                    nowSceneID = 2;
                                    //依靠标识，跳出while循环
                                    isQuitwhile = true;
                                }
                                if (nowSelIndex == 1)
                                {
                                    //退出游戏
                                    Environment.Exit(0);
                                }
                                break;
                        }
                        if (isQuitwhile)
                        {
                            break;
                        }


                    }
                    
                    
                        break;
                #endregion
                case 2:
                    #region 场景2 游戏场景
                    Console.Clear();
                    #region 不变的红墙
                    Console.ForegroundColor = ConsoleColor.Red;
                    for(int i = 0; i < w; i += 2)
                    {
                        Console.SetCursorPosition(i, 0);
                        Console.Write("■");
                        Console.SetCursorPosition(i, h - 3);
                        Console.Write("■");
                        Console.SetCursorPosition(i, h - 8);
                        Console.Write("■");
                    }
                    for(int i = 0; i < h-2;i++ )
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write("■");
                        Console.SetCursorPosition(w-2, i);
                        Console.Write("■");
                    }
                    #endregion

                    #region boss属性相关
                    int bossX=24, bossY=15;
                    int bossHp = 100;
                    int bossAtkMax = 13;
                    int bossAtkMin = 7;
                    string bossIcon = "■";
                    //声明一个颜色变量
                    ConsoleColor bossColor = ConsoleColor.Green;
                    #endregion
                    #region 玩家属性相关
                    int playerX = 4, playerY = 4;
                    int playerHp = 100;
                    int playerAtkMax = 13;
                    int playerAtkMin = 7;
                    string playerIcon = "●";
                    ConsoleColor playerColor = ConsoleColor.Yellow;
                    char playerMov;
                    #endregion
                    #region 公主相关
                    int princessX = 24, princessY = 5;
                    string princessIcon = "◆";
                    ConsoleColor princessColor = ConsoleColor.Blue;
                    #endregion
                    bool isFight =false;
                    bool isOver=false;//用来从内层switch跳出外层while循环
                    //游戏场景的死循环，专门检测玩家 输入相关逻辑
                    while (true)
                    {
                        #region boss生成
                        if (bossHp > 0)
                        {
                            Console.SetCursorPosition(bossX, bossY);
                            Console.ForegroundColor = bossColor;
                            Console.Write(bossIcon);
                        }
                        #endregion
                        #region 玩家生成
                        Console.SetCursorPosition(playerX, playerY);
                        Console.ForegroundColor = playerColor;
                        Console.Write(playerIcon);
                        #endregion
                        #region 公主生成
                        if (bossHp <= 0)
                        {
                            Console.SetCursorPosition(princessX, princessY);
                            Console.ForegroundColor = princessColor;
                            Console.Write(princessIcon);
                        }
                        
                        #endregion
                        playerMov = Console.ReadKey(true).KeyChar;
                        #region 战斗状态逻辑
                        //处于战斗状态
                        if (isFight)
                        {
                            //如果处于战斗状态  只会按J键
                            if (playerMov == 'J' || playerMov == 'j')
                            {
                                //判断玩家 或怪物 是否死亡
                                if(playerHp <= 0)
                                {
                                    //游戏结束了
                                    //切换到结束场景
                                    nowSceneID = 3;
                                    break;
                                }
                                else if(bossHp <= 0)
                                {
                                    //营救公主
                                    //擦除boss
                                    Console.SetCursorPosition(bossX, bossY);
                                    Console.Write(" ");
                                    isFight = false;
                                }
                                else
                                {
                                    //处理按J键打架
                                    //玩家打怪物
                                    Random r = new Random();
                                    int atk = r.Next(playerAtkMin,playerAtkMax);
                                    bossHp-= atk;
                                    Console.ForegroundColor=ConsoleColor.Blue;
                                    Console.SetCursorPosition(2,h-6); 
                                    Console.Write("                                   ");
                                    Console.SetCursorPosition(2, h - 6);
                                    Console.Write("你对boss造成了{0}点伤害，boss还剩{1}点血量", atk, bossHp);
                                    //怪兽打玩家
                                    if(bossHp>0)
                                    {
                                        atk = r.Next(bossAtkMin,bossAtkMax);
                                        playerHp-= atk;
                                        Console.ForegroundColor=ConsoleColor.Red;
                                        Console.SetCursorPosition(2, h - 5);
                                        Console.Write("                                           ");
                                        if (playerHp <= 0)
                                        {
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("很遗憾，您已战败");
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("boss对你造成了{0}点伤害，你还剩{1}点血量", atk, playerHp);
                                        }
                                    }
                                    else
                                    {
                                        //擦除之前的战斗信息
                                        Console.SetCursorPosition(2, h - 7);
                                        Console.Write("                                        ");
                                        Console.SetCursorPosition(2, h - 6);
                                        Console.Write("                                        ");
                                        Console.SetCursorPosition(2, h - 5);
                                        Console.Write("                                        ");
                                        //显示胜利的信息
                                        Console.SetCursorPosition(2, h - 7);
                                        Console.Write("恭喜你，战胜了boss，快去救公主");
                                        Console.SetCursorPosition(2, h - 6);
                                        Console.Write("前往公主身边，按J键继续");
                                    }
                                }
                            }
                        }
                        #endregion
                        //非战斗状态
                        else
                        {
                            #region 玩家移动相关
                            Console.SetCursorPosition(playerX, playerY);
                            Console.Write(" ");
                            switch (playerMov)
                            {
                                case 'W':
                                case 'w':
                                    playerY -= 1;
                                    if (playerY < 1)
                                    {
                                        playerY = 1;
                                    }
                                    else if (playerY == bossY && playerX == bossX && bossHp > 0)
                                    {
                                        ++playerY;
                                    }
                                    else if (playerY == princessY && playerX == princessX && bossHp <= 0)
                                    {
                                        ++playerY;
                                    }
                                        break;
                                case 'A':
                                case 'a':
                                    playerX -= 2;
                                    if (playerX < 2)
                                    {
                                        playerX = 2;
                                    }
                                    else if (playerY == bossY && playerX == bossX && bossHp > 0)
                                    {
                                        playerX += 2;
                                    }
                                    else if (playerY == princessY && playerX == princessX && bossHp <= 0)
                                    {
                                        playerX += 2;
                                    }
                                    break;
                                case 'S':
                                case 's':
                                    playerY += 1;
                                    if (playerY > h - 9)
                                    {
                                        playerY = h - 9;
                                    }
                                    else if (playerY == bossY && playerX == bossX && bossHp > 0)
                                    {
                                        --playerY;
                                    }
                                    else if (playerY == princessY && playerX == princessX && bossHp <= 0)
                                    {
                                        --playerY;
                                    }
                                    break;
                                    break;
                                case 'D':
                                case 'd':
                                    playerX += 2;
                                    if (playerX > w - 4)
                                    {
                                        playerX = w - 4;
                                    }
                                    else if (playerY == bossY && playerX == bossX && bossHp > 0)
                                    {
                                        playerX -= 2;
                                    }
                                    else if (playerY == princessY && playerX == princessX && bossHp <= 0)
                                    {
                                        playerX -= 2;
                                    }
                                    break;
                                case 'J':
                                case 'j':
                                    if ((playerY == bossY && playerX == bossX + 2 ||
                                        playerY == bossY && playerX == bossX - 2 ||
                                        playerX == bossX && playerY == bossY + 1 ||
                                        playerX == bossX && playerY == bossY - 1) && bossHp > 0)
                                    {
                                        isFight = true;
                                        Console.SetCursorPosition(2, h - 7);
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("开始战斗，按J键继续");
                                        Console.SetCursorPosition(2, h - 6);
                                        Console.WriteLine("玩家当前血量为{0}", playerHp);
                                        Console.SetCursorPosition(2, h - 5);
                                        Console.WriteLine("boss当前血量为{0}", bossHp);
                                    }
                                    if((playerY == princessY && playerX == princessX + 2 ||
                                        playerY == princessY && playerX == princessX - 2 ||
                                        playerX == princessX && playerY == princessY + 1 ||
                                        playerX == princessX && playerY == princessY - 1) && bossHp <= 0)
                                    {
                                        nowSceneID = 3;
                                        isOver = true;
                                    }
                                    break;
                            }
                            #endregion
                        }
                        if (isOver)
                        {
                            break;
                        }

                    }
                    #endregion
                    break;
                case 3:
                    #region 场景3 结束场景
                    Console.Clear();
                    //让标题居中
                    Console.SetCursorPosition(w / 2 - 4, 8);
                    Console.Write("GameOver");
                    //定义选项的标号
                    int nowSelIndex2 = 0;
                    bool isQuitwhile2;
                    while (true)
                    {
                        isQuitwhile2 = false;

                        Console.SetCursorPosition(w / 2 - 6, 13);
                        //用三目运算符，判断是否选中该选项，来更改其文字颜色
                        Console.ForegroundColor = nowSelIndex2 == 0 ? ConsoleColor.Green : ConsoleColor.White;
                        Console.Write("重新开始游戏");

                        Console.SetCursorPosition(w / 2 - 4, 15);
                        Console.ForegroundColor = nowSelIndex2 == 1 ? ConsoleColor.Green : ConsoleColor.White;
                        Console.Write("退出游戏");
                        //获取玩家输入
                        char c=Console.ReadKey(true).KeyChar;
                        switch (c)
                        {
                            case 'W':
                            case 'w':
                                --nowSelIndex2;
                                if(nowSelIndex2 < 0)
                                {
                                    nowSelIndex2 = 0;
                                }
                                break;
                            case 'S':
                            case 's':
                                ++nowSelIndex2;
                                if (nowSelIndex2 > 1)
                                {
                                    nowSelIndex2 = 1;
                                }
                                break;
                            case 'J':
                            case 'j':
                                if(nowSelIndex2 == 0)
                                {
                                    nowSceneID = 1;
                                    isQuitwhile2 |= true;
                                }
                                else if (nowSelIndex2 == 1)
                                {
                                    Environment.Exit(0);
                                }
                                break;
                        }
                        if (isQuitwhile2)
                        {
                            break;
                        }




                    }






                    #endregion
                    break;

            }
        }

        #endregion

    }
}