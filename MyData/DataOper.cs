using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Configuration;
using System.Net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.Web.UI.WebControls;

namespace MyData
{
    public class DataOper
    {

        public DataOper()
        {
            //
            //TODO:在此处添加构造函数逻辑
            //
        }

        public static string getHYJB(string jb) //会员级别
        {
            string jbname = "";
            switch (jb)
            {
                case "0":
                    jbname = "未激活";
                    break;
                case "1":
                    jbname = "会员卡";
                    break;
                case "2":
                    jbname = "贵宾卡";
                    break;
                case "3":
                    jbname = "金尊卡";
                    break;
                case "4":
                    jbname = "至尊卡";
                    break;
                default:
                    break;
            }
            return jbname;
        }
        public static string dao_name()
        {
            return "数据导出.xls";
        }
        public static void Excel_daochu(DataTable dt, string filepath)
        {
            FileStream file = new FileStream(filepath + "导出模板.xls", FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            hssfworkbook.SetSheetName(0, "数据");
            ISheet sheet1 = hssfworkbook.GetSheet("数据");
            InsertRows(sheet1, 0, dt.Rows.Count, hssfworkbook, dt.Columns.Count);
            //sheet1.GetRow(0).GetCell(0).SetCellValue("12356456456");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
               
                sheet1.GetRow(0).GetCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }
            for (int j = 1; j <= dt.Rows.Count; j++)
            {
                //dt.Columns[0].ColumnName;
                //IRow row1 = sheet1.GetRow(j);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sheet1.GetRow(j).GetCell(i).SetCellValue(dt.Rows[j - 1][i].ToString());
                }
            }

            sheet1.ForceFormulaRecalculation = true;
            file = new FileStream(filepath + dao_name(), FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
        public static void Excel_daochu(DataTable dt, string bt,string filepath)
        {
            FileStream file = new FileStream(filepath + "导出模板.xls", FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            hssfworkbook.SetSheetName(0, "数据");
            ISheet sheet1 = hssfworkbook.GetSheet("数据");
            InsertRows(sheet1, 0, dt.Rows.Count, hssfworkbook, dt.Columns.Count);
            //sheet1.GetRow(0).GetCell(0).SetCellValue("12356456456");
            string[] strs = bt.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
               
                sheet1.GetRow(0).GetCell(i).SetCellValue(strs[i].ToString());
            }
            for (int j = 1; j <= dt.Rows.Count; j++)
            {
                //dt.Columns[0].ColumnName;
                //IRow row1 = sheet1.GetRow(j);
                for (int i = 0; i < strs.Length; i++)
                {
                    sheet1.GetRow(j).GetCell(i).SetCellValue(dt.Rows[j - 1][i].ToString());
                }
            }

            sheet1.ForceFormulaRecalculation = true;
            file = new FileStream(filepath + dao_name(), FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
        public static void InsertRows(ISheet targetSheet, int fromRowIndex, int rowCount, HSSFWorkbook hssfworkbook, int columnCount)
        {
            IRow xxx = targetSheet.GetRow(fromRowIndex);
            for (int rowIndex = fromRowIndex; rowIndex <= fromRowIndex + rowCount; rowIndex++)
            {
                IRow rowInsert = targetSheet.CreateRow(rowIndex);
                //rowInsert.RowStyle = xxx.RowStyle;
                //rowInsert.Height = xxx.Height;
                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    ICell cellInsert = rowInsert.CreateCell(colIndex);
                    // cellInsert.CellStyle = xxx.GetCell(colIndex).CellStyle;
                }
            }
        }

        #region 加密函数

        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="pass">原始密码</param>
        /// <returns></returns>
        public static string encryptmd5(string pass)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "md5");
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <param name="pass">原始密码</param>
        /// <returns></returns>
        public static string encryptsha1(string pass)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "sha1");
        }


        /// <summary>
        /// 对密码进行des加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string getDesEncryptPassword(string password)
        {
            //实例化des加密算法的对象
            DESCryptoServiceProvider aa = new DESCryptoServiceProvider();
            aa.Key = ASCIIEncoding.ASCII.GetBytes("fGyu+_4#");
            aa.IV = ASCIIEncoding.ASCII.GetBytes("fGyu+_4#");
            //得到一个加密对象
            ICryptoTransform bb = aa.CreateEncryptor();

            byte[] b = Encoding.UTF8.GetBytes(password);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, bb, CryptoStreamMode.Write);
            cs.Write(b, 0, b.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }


        /// <summary>
        /// 对密码进行des解密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string getBankTrans(string password)
        {
            if (password.Trim() != "")
            {
                try
                {
                    //实例化des加密算法的对象
                    DESCryptoServiceProvider aa = new DESCryptoServiceProvider();
                    aa.Key = ASCIIEncoding.ASCII.GetBytes("fGyu+_4#");
                    aa.IV = ASCIIEncoding.ASCII.GetBytes("fGyu+_4#");
                    //生成解密算法
                    ICryptoTransform bb = aa.CreateDecryptor();
                    byte[] b1 = new byte[password.Length];
                    //转换转换后的值
                    //0101,0102,0201,0202,0207,
                    
                    b1 = Convert.FromBase64String(password);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, bb, CryptoStreamMode.Write);
                    cs.Write(b1, 0, b1.Length);
                    cs.FlushFinalBlock();
                    Encoding ed = new UTF8Encoding();
                    return ed.GetString(ms.ToArray());
                }
                catch
                {
                    return "";
                }
            }
            else
                return "";
        }

        /// <summary>
        /// 转ASCII码字符串
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static string CHR(int Data)
        {
            //string str;
            //ASCIIEncoding AE2 = new ASCIIEncoding();
            //byte[] ByteArray2 = { data };
            //char[] CharArray = AE2.GetChars(ByteArray2);
            //str = CharArray[0];
            //return str;

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = new byte[] { byte.Parse(Data.ToString()) };
            return encoding.GetChars(bytes)[0].ToString();
        }

        /// <summary>
        /// 跳转页面参数加密
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getAscii(string code)
        {
            string str = "";
            if (setDBNull(code) != "")
            {
                for (int i = 0; i < code.Length; i++)
                {
                    str = str + CHR(ASC(code.Substring(i, 1)) + 50);
                }
            }
            return str;
        }

        /// <summary>
        /// 跳转页面参数解密
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getAsciiJM(string code)
        {
            string str = "";
            if (setDBNull(code) != "")
            {
                for (int i = 0; i < code.Length; i++)
                {
                    str = str + CHR(ASC(code.Substring(i, 1)) - 50);
                }
            }
            return str;
        }

        #endregion

        #region 常用函数

        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string setTrueString(string str)
        {
            str = str.Replace("'", "''");
            str = str.Replace("\"", "“");
            str = str.Replace("--", "－－");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("%", "");
            return str.Trim();
        }

        /// <summary>
        /// 文本编辑器转换字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string setTrueStringa(string str)
        {
            str = str.Replace("'", "''");
            str = str.Replace("--", "－－");
            return str;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="v">字符串</param>
        /// <param name="delimstr">分割符</param>
        /// <returns></returns>
        public static string[] Split(string v, string delimstr)
        {
            string[] a = null;
            string delimStr = delimstr;
            char[] delimiter = delimStr.ToCharArray();
            a = v.Split(delimiter);
            return a;
        }


        /// <summary>
        /// 取汉字的拼音码
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        public static string GetFirstLetter(string hz)
        {

            string ls_second_eng = "CJWGNSPGCGNESYPBTYYZDXYKYGTDJNNJQMBSGZSCYJSYYQPGKBZGYCYWJKGKLJSWKPJQHYTWDDZLSGMRYPYWWCCKZNKYDGTTNGJEYKKZYTCJNMCYLQLYPYQFQRPZSLWBTGKJFYXJWZLTBNCXJJJJZXDTTSQZYCDXXHGCKBPHFFSSWYBGMXLPBYLLLHLXSPZMYJHSOJNGHDZQYKLGJHSGQZHXQGKEZZWYSCSCJXYEYXADZPMDSSMZJZQJYZCDJZWQJBDZBXGZNZCPWHKXHQKMWFBPBYDTJZZKQHYLYGXFPTYJYYZPSZLFCHMQSHGMXXSXJJSDCSBBQBEFSJYHWWGZKPYLQBGLDLCCTNMAYDDKSSNGYCSGXLYZAYBNPTSDKDYLHGYMYLCXPYCJNDQJWXQXFYYFJLEJBZRXCCQWQQSBNKYMGPLBMJRQCFLNYMYQMSQTRBCJTHZTQFRXQ" +
                "HXMJJCJLXQGJMSHZKBSWYEMYLTXFSYDSGLYCJQXSJNQBSCTYHBFTDCYZDJWYGHQFRXWCKQKXEBPTLPXJZSRMEBWHJLBJSLYYSMDXLCLQKXLHXJRZJMFQHXHWYWSBHTRXXGLHQHFNMNYKLDYXZPWLGGTMTCFPAJJZYLJTYANJGBJPLQGDZYQYAXBKYSECJSZNSLYZHZXLZCGHPXZHZNYTDSBCJKDLZAYFMYDLEBBGQYZKXGLDNDNYSKJSHDLYXBCGHXYPKDJMMZNGMMCLGWZSZXZJFZNMLZZTHCSYDBDLLSCDDNLKJYKJSYCJLKOHQASDKNHCSGANHDAASHTCPLCPQYBSDMPJLPCJOQLCDHJJYSPRCHNWJNLHLYYQYYWZPTCZGWWMZFFJQQQQYXACLBHKDJXDGMMYDJXZLLSYGXGKJRYWZWYCLZMSSJZLDBYDCFCXYHLXCHYZJQSFQAGMNYXPFRKSSB" +
                "JLYXYSYGLNSCMHCWWMNZJJLXXHCHSYDSTTXRYCYXBYHCSMXJSZNPWGPXXTAYBGAJCXLYSDCCWZOCWKCCSBNHCPDYZNFCYYTYCKXKYBSQKKYTQQXFCWCHCYKELZQBSQYJQCCLMTHSYWHMKTLKJLYCXWHEQQHTQHZPQSQSCFYMMDMGBWHWLGSSLYSDLMLXPTHMJHWLJZYHZJXHTXJLHXRSWLWZJCBXMHZQXSDZPMGFCSGLSXYMJSHXPJXWMYQKSMYPLRTHBXFTPMHYXLCHLHLZYLXGSSSSTCLSLDCLRPBHZHXYYFHBBGDMYCNQQWLQHJJZYWJZYEJJDHPBLQXTQKWHLCHQXAGTLXLJXMSLXHTZKZJECXJCJNMFBYCSFYWYBJZGNYSDZSQYRSLJPCLPWXSDWEJBJCBCNAYTWGMPAPCLYQPCLZXSBNMSGGFNZJJBZSFZYNDXHPLQKZCZWALSBCCJXJYZGWKYP" +
                "SGXFZFCDKHJGXDLQFSGDSLQWZKXTMHSBGZMJZRGLYJBPMLMSXLZJQQHZYJCZYDJWBMJKLDDPMJEGXYHYLXHLQYQHKYCWCJMYYXNATJHYCCXZPCQLBZWWYTWBQCMLPMYRJCCCXFPZNZZLJPLXXYZTZLGDLDCKLYRZZGQTGJHHHJLJAXFGFJZSLCFDQZLCLGJDJCSNCLLJPJQDCCLCJXMYZFTSXGCGSBRZXJQQCTZHGYQTJQQLZXJYLYLBCYAMCSTYLPDJBYREGKLZYZHLYSZQLZNWCZCLLWJQJJJKDGJZOLBBZPPGLGHTGZXYGHZMYCNQSYCYHBHGXKAMTXYXNBSKYZZGJZLQJDFCJXDYGJQJJPMGWGJJJPKQSBGBMMCJSSCLPQPDXCDYYKYFCJDDYYGYWRHJRTGZNYQLDKLJSZZGZQZJGDYKSHPZMTLCPWNJAFYZDJCNMWESCYGLBTZCGMSSLLYXQSXSBSJS" +
                "BBSGGHFJLWPMZJNLYYWDQSHZXTYYWHMCYHYWDBXBTLMSYYYFSXJCSDXXLHJHFSSXZQHFZMZCZTQCXZXRTTDJHNNYZQQMNQDMMGYYDXMJGDHCDYZBFFALLZTDLTFXMXQZDNGWQDBDCZJDXBZGSQQDDJCMBKZFFXMKDMDSYYSZCMLJDSYNSPRSKMKMPCKLGDBQTFZSWTFGGLYPLLJZHGJJGYPZLTCSMCNBTJBQFKTHBYZGKPBBYMTTSSXTBNPDKLEYCJNYCDYKZDDHQHSDZSCTARLLTKZLGECLLKJLQJAQNBDKKGHPJTZQKSECSHALQFMMGJNLYJBBTMLYZXDCJPLDLPCQDHZYCBZSCZBZMSLJFLKRZJSNFRGJHXPDHYJYBZGDLQCSEZGXLBLGYXTWMABCHECMWYJYZLLJJYHLGBDJLSLYGKDZPZXJYYZLWCXSZFGWYYDLYHCLJSCMBJHBLYZLYCBLYDPDQYSXQZB" +
                "YTDKYXJYYCNRJMDJGKLCLJBCTBJDDBBLBLCZQRPXJCGLZCSHLTOLJNMDDDLNGKAQHQHJGYKHEZNMSHRPHQQJCHGMFPRXHJGDYCHGHLYRZQLCYQJNZSQTKQJYMSZSWLCFQQQXYFGGYPTQWLMCRNFKKFSYYLQBMQAMMMYXCTPSHCPTXXZZSMPHPSHMCLMLDQFYQXSZYJDJJZZHQPDSZGLSTJBCKBXYQZJSGPSXQZQZRQTBDKYXZKHHGFLBCSMDLDGDZDBLZYYCXNNCSYBZBFGLZZXSWMSCCMQNJQSBDQSJTXXMBLTXZCLZSHZCXRQJGJYLXZFJPHYMZQQYDFQJJLZZNZJCDGZYGCTXMZYSCTLKPHTXHTLBJXJLXSCDQXCBBTJFQZFSLTJBTKQBXXJJLJCHCZDBZJDCZJDCPRNPQCJPFCZLCLZXZDMXMPHJSGZGSZZQJYLWTJPFSYASMCJBTZKYCWMYTCSJJLJCQLWZM" +
                "ALBXYFBPNLSFHTGJWEJJXXGLLJSTGSHJQLZFKCGNNDSZFDEQFHBSAQTGLLBXMMYGSZLDYDQMJJRGBJTKGDHGKBLQKBDMBYLXWCXYTTYBKMRTJZXQJBHLMHMJJZMQASLDCYXYQDLQCAFYWYXQHZ";
            string ls_second_ch = "亍丌兀丐廿卅丕亘丞鬲孬噩丨禺丿匕乇夭爻卮氐囟胤馗毓睾鼗丶亟" +
                "鼐乜乩亓芈孛啬嘏仄厍厝厣厥厮靥赝匚叵匦匮匾赜卦卣刂刈刎刭刳刿剀剌剞剡剜蒯剽劂劁劐劓冂罔亻仃仉仂仨仡仫仞伛仳伢佤仵伥伧伉伫佞佧攸佚佝佟佗伲伽佶佴侑侉侃侏佾佻侪佼侬侔俦俨俪俅俚俣俜俑俟俸倩偌俳倬倏倮倭俾倜倌倥倨偾偃偕偈偎偬偻傥傧傩傺僖儆僭僬僦僮儇儋仝氽佘佥俎龠汆籴兮巽黉馘冁夔勹匍訇匐凫夙兕亠兖亳衮袤亵脔裒禀嬴蠃羸冫冱冽冼凇冖冢冥讠讦讧讪讴讵讷诂诃诋诏诎诒诓诔诖诘诙诜诟诠诤诨诩诮诰诳诶诹诼诿谀谂谄谇谌谏谑谒谔谕谖谙谛谘谝谟谠谡谥谧谪谫谮谯谲谳谵谶卩卺阝阢阡阱阪阽阼" +
                "陂陉陔陟陧陬陲陴隈隍隗隰邗邛邝邙邬邡邴邳邶邺邸邰郏郅邾郐郄郇郓郦郢郜郗郛郫郯郾鄄鄢鄞鄣鄱鄯鄹酃酆刍奂劢劬劭劾哿勐勖勰叟燮矍廴凵凼鬯厶弁畚巯坌垩垡塾墼壅壑圩圬圪圳圹圮圯坜圻坂坩垅坫垆坼坻坨坭坶坳垭垤垌垲埏垧垴垓垠埕埘埚埙埒垸埴埯埸埤埝堋堍埽埭堀堞堙塄堠塥塬墁墉墚墀馨鼙懿艹艽艿芏芊芨芄芎芑芗芙芫芸芾芰苈苊苣芘芷芮苋苌苁芩芴芡芪芟苄苎芤苡茉苷苤茏茇苜苴苒苘茌苻苓茑茚茆茔茕苠苕茜荑荛荜茈莒茼茴茱莛荞茯荏荇荃荟荀茗荠茭茺茳荦荥荨茛荩荬荪荭荮莰荸莳莴莠莪莓莜莅荼莶莩荽莸荻" +
                "莘莞莨莺莼菁萁菥菘堇萘萋菝菽菖萜萸萑萆菔菟萏萃菸菹菪菅菀萦菰菡葜葑葚葙葳蒇蒈葺蒉葸萼葆葩葶蒌蒎萱葭蓁蓍蓐蓦蒽蓓蓊蒿蒺蓠蒡蒹蒴蒗蓥蓣蔌甍蔸蓰蔹蔟蔺蕖蔻蓿蓼蕙蕈蕨蕤蕞蕺瞢蕃蕲蕻薤薨薇薏蕹薮薜薅薹薷薰藓藁藜藿蘧蘅蘩蘖蘼廾弈夼奁耷奕奚奘匏尢尥尬尴扌扪抟抻拊拚拗拮挢拶挹捋捃掭揶捱捺掎掴捭掬掊捩掮掼揲揸揠揿揄揞揎摒揆掾摅摁搋搛搠搌搦搡摞撄摭撖摺撷撸撙撺擀擐擗擤擢攉攥攮弋忒甙弑卟叱叽叩叨叻吒吖吆呋呒呓呔呖呃吡呗呙吣吲咂咔呷呱呤咚咛咄呶呦咝哐咭哂咴哒咧咦哓哔呲咣哕咻咿哌哙哚哜咩" +
                "咪咤哝哏哞唛哧唠哽唔哳唢唣唏唑唧唪啧喏喵啉啭啁啕唿啐唼唷啖啵啶啷唳唰啜喋嗒喃喱喹喈喁喟啾嗖喑啻嗟喽喾喔喙嗪嗷嗉嘟嗑嗫嗬嗔嗦嗝嗄嗯嗥嗲嗳嗌嗍嗨嗵嗤辔嘞嘈嘌嘁嘤嘣嗾嘀嘧嘭噘嘹噗嘬噍噢噙噜噌噔嚆噤噱噫噻噼嚅嚓嚯囔囗囝囡囵囫囹囿圄圊圉圜帏帙帔帑帱帻帼帷幄幔幛幞幡岌屺岍岐岖岈岘岙岑岚岜岵岢岽岬岫岱岣峁岷峄峒峤峋峥崂崃崧崦崮崤崞崆崛嵘崾崴崽嵬嵛嵯嵝嵫嵋嵊嵩嵴嶂嶙嶝豳嶷巅彳彷徂徇徉後徕徙徜徨徭徵徼衢彡犭犰犴犷犸狃狁狎狍狒狨狯狩狲狴狷猁狳猃狺狻猗猓猡猊猞猝猕猢猹猥猬猸猱獐獍獗獠獬獯獾" +
                "舛夥飧夤夂饣饧饨饩饪饫饬饴饷饽馀馄馇馊馍馐馑馓馔馕庀庑庋庖庥庠庹庵庾庳赓廒廑廛廨廪膺忄忉忖忏怃忮怄忡忤忾怅怆忪忭忸怙怵怦怛怏怍怩怫怊怿怡恸恹恻恺恂恪恽悖悚悭悝悃悒悌悛惬悻悱惝惘惆惚悴愠愦愕愣惴愀愎愫慊慵憬憔憧憷懔懵忝隳闩闫闱闳闵闶闼闾阃阄阆阈阊阋阌阍阏阒阕阖阗阙阚丬爿戕氵汔汜汊沣沅沐沔沌汨汩汴汶沆沩泐泔沭泷泸泱泗沲泠泖泺泫泮沱泓泯泾洹洧洌浃浈洇洄洙洎洫浍洮洵洚浏浒浔洳涑浯涞涠浞涓涔浜浠浼浣渚淇淅淞渎涿淠渑淦淝淙渖涫渌涮渫湮湎湫溲湟溆湓湔渲渥湄滟溱溘滠漭滢溥溧溽溻溷滗溴滏溏滂" +
                "溟潢潆潇漤漕滹漯漶潋潴漪漉漩澉澍澌潸潲潼潺濑濉澧澹澶濂濡濮濞濠濯瀚瀣瀛瀹瀵灏灞宀宄宕宓宥宸甯骞搴寤寮褰寰蹇謇辶迓迕迥迮迤迩迦迳迨逅逄逋逦逑逍逖逡逵逶逭逯遄遑遒遐遨遘遢遛暹遴遽邂邈邃邋彐彗彖彘尻咫屐屙孱屣屦羼弪弩弭艴弼鬻屮妁妃妍妩妪妣妗姊妫妞妤姒妲妯姗妾娅娆姝娈姣姘姹娌娉娲娴娑娣娓婀婧婊婕娼婢婵胬媪媛婷婺媾嫫媲嫒嫔媸嫠嫣嫱嫖嫦嫘嫜嬉嬗嬖嬲嬷孀尕尜孚孥孳孑孓孢驵驷驸驺驿驽骀骁骅骈骊骐骒骓骖骘骛骜骝骟骠骢骣骥骧纟纡纣纥纨纩纭纰纾绀绁绂绉绋绌绐绔绗绛绠绡绨绫绮绯绱绲缍绶绺绻绾缁缂缃" +
                "缇缈缋缌缏缑缒缗缙缜缛缟缡缢缣缤缥缦缧缪缫缬缭缯缰缱缲缳缵幺畿巛甾邕玎玑玮玢玟珏珂珑玷玳珀珉珈珥珙顼琊珩珧珞玺珲琏琪瑛琦琥琨琰琮琬琛琚瑁瑜瑗瑕瑙瑷瑭瑾璜璎璀璁璇璋璞璨璩璐璧瓒璺韪韫韬杌杓杞杈杩枥枇杪杳枘枧杵枨枞枭枋杷杼柰栉柘栊柩枰栌柙枵柚枳柝栀柃枸柢栎柁柽栲栳桠桡桎桢桄桤梃栝桕桦桁桧桀栾桊桉栩梵梏桴桷梓桫棂楮棼椟椠棹椤棰椋椁楗棣椐楱椹楠楂楝榄楫榀榘楸椴槌榇榈槎榉楦楣楹榛榧榻榫榭槔榱槁槊槟榕槠榍槿樯槭樗樘橥槲橄樾檠橐橛樵檎橹樽樨橘橼檑檐檩檗檫猷獒殁殂殇殄殒殓殍殚殛殡殪轫轭轱轲轳轵轶" +
                "轸轷轹轺轼轾辁辂辄辇辋辍辎辏辘辚軎戋戗戛戟戢戡戥戤戬臧瓯瓴瓿甏甑甓攴旮旯旰昊昙杲昃昕昀炅曷昝昴昱昶昵耆晟晔晁晏晖晡晗晷暄暌暧暝暾曛曜曦曩贲贳贶贻贽赀赅赆赈赉赇赍赕赙觇觊觋觌觎觏觐觑牮犟牝牦牯牾牿犄犋犍犏犒挈挲掰搿擘耄毪毳毽毵毹氅氇氆氍氕氘氙氚氡氩氤氪氲攵敕敫牍牒牖爰虢刖肟肜肓肼朊肽肱肫肭肴肷胧胨胩胪胛胂胄胙胍胗朐胝胫胱胴胭脍脎胲胼朕脒豚脶脞脬脘脲腈腌腓腴腙腚腱腠腩腼腽腭腧塍媵膈膂膑滕膣膪臌朦臊膻臁膦欤欷欹歃歆歙飑飒飓飕飙飚殳彀毂觳斐齑斓於旆旄旃旌旎旒旖炀炜炖炝炻烀炷炫炱烨烊焐焓焖焯焱" +
                "煳煜煨煅煲煊煸煺熘熳熵熨熠燠燔燧燹爝爨灬焘煦熹戾戽扃扈扉礻祀祆祉祛祜祓祚祢祗祠祯祧祺禅禊禚禧禳忑忐怼恝恚恧恁恙恣悫愆愍慝憩憝懋懑戆肀聿沓泶淼矶矸砀砉砗砘砑斫砭砜砝砹砺砻砟砼砥砬砣砩硎硭硖硗砦硐硇硌硪碛碓碚碇碜碡碣碲碹碥磔磙磉磬磲礅磴礓礤礞礴龛黹黻黼盱眄眍盹眇眈眚眢眙眭眦眵眸睐睑睇睃睚睨睢睥睿瞍睽瞀瞌瞑瞟瞠瞰瞵瞽町畀畎畋畈畛畲畹疃罘罡罟詈罨罴罱罹羁罾盍盥蠲钅钆钇钋钊钌钍钏钐钔钗钕钚钛钜钣钤钫钪钭钬钯钰钲钴钶钷钸钹钺钼钽钿铄铈铉铊铋铌铍铎铐铑铒铕铖铗铙铘铛铞铟铠铢铤铥铧铨铪铩铫铮铯铳铴铵铷铹铼" +
                "铽铿锃锂锆锇锉锊锍锎锏锒锓锔锕锖锘锛锝锞锟锢锪锫锩锬锱锲锴锶锷锸锼锾锿镂锵镄镅镆镉镌镎镏镒镓镔镖镗镘镙镛镞镟镝镡镢镤镥镦镧镨镩镪镫镬镯镱镲镳锺矧矬雉秕秭秣秫稆嵇稃稂稞稔稹稷穑黏馥穰皈皎皓皙皤瓞瓠甬鸠鸢鸨鸩鸪鸫鸬鸲鸱鸶鸸鸷鸹鸺鸾鹁鹂鹄鹆鹇鹈鹉鹋鹌鹎鹑鹕鹗鹚鹛鹜鹞鹣鹦鹧鹨鹩鹪鹫鹬鹱鹭鹳疒疔疖疠疝疬疣疳疴疸痄疱疰痃痂痖痍痣痨痦痤痫痧瘃痱痼痿瘐瘀瘅瘌瘗瘊瘥瘘瘕瘙瘛瘼瘢瘠癀瘭瘰瘿瘵癃瘾瘳癍癞癔癜癖癫癯翊竦穸穹窀窆窈窕窦窠窬窨窭窳衤衩衲衽衿袂裆袷袼裉裢裎裣裥裱褚裼裨裾裰褡褙褓褛褊褴褫褶襁襦疋胥皲皴矜耒" +
                "耔耖耜耠耢耥耦耧耩耨耱耋耵聃聆聍聒聩聱覃顸颀颃颉颌颍颏颔颚颛颞颟颡颢颥颦虍虔虬虮虿虺虼虻蚨蚍蚋蚬蚝蚧蚣蚪蚓蚩蚶蛄蚵蛎蚰蚺蚱蚯蛉蛏蚴蛩蛱蛲蛭蛳蛐蜓蛞蛴蛟蛘蛑蜃蜇蛸蜈蜊蜍蜉蜣蜻蜞蜥蜮蜚蜾蝈蜴蜱蜩蜷蜿螂蜢蝽蝾蝻蝠蝰蝌蝮螋蝓蝣蝼蝤蝙蝥螓螯螨蟒蟆螈螅螭螗螃螫蟥螬螵螳蟋蟓螽蟑蟀蟊蟛蟪蟠蟮蠖蠓蟾蠊蠛蠡蠹蠼缶罂罄罅舐竺竽笈笃笄笕笊笫笏筇笸笪笙笮笱笠笥笤笳笾笞筘筚筅筵筌筝筠筮筻筢筲筱箐箦箧箸箬箝箨箅箪箜箢箫箴篑篁篌篝篚篥篦篪簌篾篼簏簖簋簟簪簦簸籁籀臾舁舂舄臬衄舡舢舣舭舯舨舫舸舻舳舴舾艄艉艋艏艚艟艨衾袅袈裘裟襞羝羟" +
                "羧羯羰羲籼敉粑粝粜粞粢粲粼粽糁糇糌糍糈糅糗糨艮暨羿翎翕翥翡翦翩翮翳糸絷綦綮繇纛麸麴赳趄趔趑趱赧赭豇豉酊酐酎酏酤酢酡酰酩酯酽酾酲酴酹醌醅醐醍醑醢醣醪醭醮醯醵醴醺豕鹾趸跫踅蹙蹩趵趿趼趺跄跖跗跚跞跎跏跛跆跬跷跸跣跹跻跤踉跽踔踝踟踬踮踣踯踺蹀踹踵踽踱蹉蹁蹂蹑蹒蹊蹰蹶蹼蹯蹴躅躏躔躐躜躞豸貂貊貅貘貔斛觖觞觚觜觥觫觯訾謦靓雩雳雯霆霁霈霏霎霪霭霰霾龀龃龅龆龇龈龉龊龌黾鼋鼍隹隼隽雎雒瞿雠銎銮鋈錾鍪鏊鎏鐾鑫鱿鲂鲅鲆鲇鲈稣鲋鲎鲐鲑鲒鲔鲕鲚鲛鲞鲟鲠鲡鲢鲣鲥鲦鲧鲨鲩鲫鲭鲮鲰鲱鲲鲳鲴鲵鲶鲷鲺鲻鲼鲽鳄鳅鳆鳇鳊鳋鳌鳍鳎鳏鳐鳓鳔" +
                "鳕鳗鳘鳙鳜鳝鳟鳢靼鞅鞑鞒鞔鞯鞫鞣鞲鞴骱骰骷鹘骶骺骼髁髀髅髂髋髌髑魅魃魇魉魈魍魑飨餍餮饕饔髟髡髦髯髫髻髭髹鬈鬏鬓鬟鬣麽麾縻麂麇麈麋麒鏖麝麟黛黜黝黠黟黢黩黧黥黪黯鼢鼬鼯鼹鼷鼽鼾齄";
            byte[] array = new byte[2];

            string return_py = "";
            for (int i = 0; i < hz.Length; i++)
            {
                array = System.Text.Encoding.Default.GetBytes(hz[i].ToString());
                if (array[0] < 176) //.非汉字
                {
                    return_py += hz[i];
                }
                else if (array[0] >= 176 && array[0] <= 215) //一级汉字
                {

                    if (hz[i].ToString().CompareTo("匝") >= 0)
                        return_py += "z";
                    else if (hz[i].ToString().CompareTo("压") >= 0)
                        return_py += "y";
                    else if (hz[i].ToString().CompareTo("昔") >= 0)
                        return_py += "x";
                    else if (hz[i].ToString().CompareTo("挖") >= 0)
                        return_py += "w";
                    else if (hz[i].ToString().CompareTo("塌") >= 0)
                        return_py += "t";
                    else if (hz[i].ToString().CompareTo("撒") >= 0)
                        return_py += "s";
                    else if (hz[i].ToString().CompareTo("然") >= 0)
                        return_py += "r";
                    else if (hz[i].ToString().CompareTo("期") >= 0)
                        return_py += "q";
                    else if (hz[i].ToString().CompareTo("啪") >= 0)
                        return_py += "p";
                    else if (hz[i].ToString().CompareTo("哦") >= 0)
                        return_py += "o";
                    else if (hz[i].ToString().CompareTo("拿") >= 0)
                        return_py += "n";
                    else if (hz[i].ToString().CompareTo("妈") >= 0)
                        return_py += "m";
                    else if (hz[i].ToString().CompareTo("垃") >= 0)
                        return_py += "l";
                    else if (hz[i].ToString().CompareTo("喀") >= 0)
                        return_py += "k";
                    else if (hz[i].ToString().CompareTo("击") >= 0)
                        return_py += "j";
                    else if (hz[i].ToString().CompareTo("哈") >= 0)
                        return_py += "h";
                    else if (hz[i].ToString().CompareTo("噶") >= 0)
                        return_py += "g";
                    else if (hz[i].ToString().CompareTo("发") >= 0)
                        return_py += "f";
                    else if (hz[i].ToString().CompareTo("蛾") >= 0)
                        return_py += "e";
                    else if (hz[i].ToString().CompareTo("搭") >= 0)
                        return_py += "d";
                    else if (hz[i].ToString().CompareTo("擦") >= 0)
                        return_py += "c";
                    else if (hz[i].ToString().CompareTo("芭") >= 0)
                        return_py += "b";
                    else if (hz[i].ToString().CompareTo("啊") >= 0)
                        return_py += "a";
                }
                else if (array[0] >= 215) //二级汉字
                {
                    return_py += ls_second_eng.Substring(ls_second_ch.IndexOf(hz[i].ToString(), 0), 1);
                }
            }
            return return_py.ToUpper();
        }



        /// <summary>
        /// 判断是否是整数
        /// </summary>
        /// <param name="strNumber">字符串</param>
        /// <returns></returns>
        public static bool IsNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }


        /// <summary>
        /// 盘点是否是浮点数
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsFloat(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");

            if (!(!objNotPositivePattern.IsMatch(strNumber) && objPositivePattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber)))
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 填充list的选择职
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        public static void ListValue(System.Web.UI.WebControls.DropDownList list, string value)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                if (list.Items[i].Value == value)
                {
                    list.SelectedIndex = i;
                    return;
                }
            }
        }

        /// <summary>
        /// 返回字符串长度
        /// </summary>
        /// <param name="str"></param>
        public static int strLength(string str)
        {
            byte[] mybyte;
            mybyte = System.Text.Encoding.Default.GetBytes(str);
            return mybyte.Length;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="stringToSub"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetFirstString(string stringToSub, int length)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            bool isCut = false;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }

                if (nLength > length)
                {
                    isCut = true;
                    break;
                }
            }
            if (isCut)
                return sb.ToString() + "...";
            else
                return sb.ToString();
        }

        public static string setDBNull(object dr)
        {
            if (dr == DBNull.Value)
                return "";
            else
                return dr.ToString();
        }

        /// <summary>
        /// 合并GridView中某列相同信息的行（单元格） 
        /// </summary>
        /// <param name="GridView1">GridView</param>
        /// <param name="cellNum">第几列</param>
        public static void GroupRows(GridView GridView1, int cellNum)
        {
            int i = 0, rowSpanNum = 1;
            while (i < GridView1.Rows.Count - 1)
            {
                GridViewRow gvr = GridView1.Rows[i];

                for (++i; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gvrNext = GridView1.Rows[i];
                    if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                    {
                        gvrNext.Cells[cellNum].Visible = false;
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                        rowSpanNum = 1;
                        break;
                    }

                    if (i == GridView1.Rows.Count - 1)
                    {
                        gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    }
                }
            }
        }

        #endregion

        #region 各种算法

        /// <summary>
        /// 返回流水序号
        /// </summary>
        /// <param name="xh"></param>
        /// <returns>流水序号</returns>
        private static string getxh(int xh,int len)
        {
            string num;

            num = xh.ToString();
            while (num.Length - len < 0)
            {
                num = "0" + num;
            }
            return num;
        }

        /// <summary>
        /// 返回日期字符串
        /// </summary>
        /// <returns>日期字符串(YYMMDD)</returns>
        private static string getrq()
        {
            string year = System.DateTime.Today.Year.ToString().Substring(2, 2);
            string month = System.DateTime.Today.Month.ToString();
            string day = System.DateTime.Today.Day.ToString();

            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            return year + month + day;
        }

        public static int ASC(String Data) //获取ASC码
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(Data);
            int p = 0;

            if (b.Length == 1) //如果为英文字符直接返回
                return (int)b[0];
            for (int i = 0; i < b.Length; i += 2)
            {
                p = (int)b[i];
                p = p * 256 + b[i + 1] - 65536;
            }
            return p;
        }
        /// <summary>
        /// 36进制算法
        /// </summary>
        /// <param name="ws">返回数的位数</param>
        /// <param name="pid">最大序号</param>
        /// <returns></returns>
        public static string getxh(int ws, string pid)
        {
            string[] number = new string[ws];
            string restr = pid;
            int i;
            if (pid.Length < ws)
            {
                for (i = pid.Length; i < ws; i++)
                {
                    restr = "0" + restr;
                }
            }
            for (i = 0; i < ws; i++)
            {
                number[i] = restr.ToUpper().Substring(restr.Length - i - 1, 1);
            }
            restr = "";
            int p = 1;
            for (i = 0; i < ws; i++)
            {
                int asci = ASC(number[i]);
                if (asci > 47 & asci < 58)
                {
                    asci += p;
                    p = 0;
                    if (asci > 57)
                        asci = ASC("A");
                }
                else if (asci >= 65 & asci <= 90)
                {
                    asci += p;
                    p = 0;
                    if (asci > 90)
                    {
                        asci = ASC("0");
                        p = 1;
                    }
                }
                number[i] = ((char)asci).ToString();
                restr = number[i] + restr;
            }
            return restr;
        }

        #endregion

        #region 流水号管理
        /// <summary>
        /// 会员编号
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="lm"></param>
        /// <returns></returns>
        public static string getlsh_HYBH()
        {

            string tablename = "会员编号";
            string lm = "会员编号";
            //string qz = "2101";
            OleDbConnection conn = DataBase.Conn();
            OleDbCommand cmd;
            string rq = "800";					//日期
            int num;					//序号
            string rnum = "";
            conn.Open();
            try
            {
                cmd = new OleDbCommand("select count(*) from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'", conn);
                num = int.Parse(cmd.ExecuteScalar().ToString());
                if (num > 0)
                {
                    int xh;
                    cmd.CommandText = "select xh from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'";
                    xh = int.Parse(cmd.ExecuteScalar().ToString());
                    int xxx = xh + 1;
                    cmd.CommandText = "update sys_lshglb set xh=" + xxx.ToString() + " where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'";
                    cmd.ExecuteNonQuery();
                    rnum = rq + getxh(xh + 1,5);
                }
                else
                {
                    cmd.CommandText = "insert into sys_lshglb(xh,bm,lm,rq) values(1314,'" + tablename + "','" + lm + "','" + rq + "')";
                    cmd.ExecuteNonQuery();
                    rnum = rq + "01314";
                }
            }
            catch
            {

            }
            conn.Close();
            return rnum;
        }
        public static string getlsh_HYBM(string tjrbm)
        {

           string tablename = "会员编码";
           string lm = tjrbm;

            OleDbConnection conn = DataBase.Conn();
            OleDbCommand cmd;
            string rq;					//日期
            int num;					//序号
            string rnum = "";
            rq = tjrbm;
            conn.Open();
            try
            {
                cmd = new OleDbCommand("select count(*) from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'", conn);
                num = int.Parse(cmd.ExecuteScalar().ToString());
                if (num > 0)
                {
                    int xh;
                    cmd.CommandText = "select xh from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'";
                    xh = int.Parse(cmd.ExecuteScalar().ToString());
                    int xxx = xh + 1;
                    cmd.CommandText = "update sys_lshglb set xh=" + xxx.ToString() + " where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'";
                    cmd.ExecuteNonQuery();
                    rnum = rq + getxh(xh + 1, 4);
                }
                else
                {
                    cmd.CommandText = "insert into sys_lshglb(xh,bm,lm,rq) values(1,'" + tablename + "','" + lm + "','" + rq + "')";
                    cmd.ExecuteNonQuery();
                    rnum = rq + "0001";
                }
            }
            catch
            {

            }
            conn.Close();
            return rnum;
        }

        /// <summary>
        /// 取得流水号
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="lm">列名</param>
        /// <returns>流水号</returns>
        public static string getlsh(string tablename, string lm)
        {

            tablename = tablename.ToLower();
            lm = lm.ToLower();

            OleDbConnection conn = DataBase.Conn();
            OleDbCommand cmd;
            string rq;					//日期
            int num;					//序号
            string rnum = "";
            rq = getrq();
            conn.Open();
            try
            {
                cmd = new OleDbCommand("select count(*) from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'", conn);
                num = int.Parse(cmd.ExecuteScalar().ToString());
                if (num > 0)
                {
                    int xh;
                    cmd.CommandText = "select xh from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'";
                    xh = int.Parse(cmd.ExecuteScalar().ToString());
                    int xxx = xh + 1;
                    cmd.CommandText = "update sys_lshglb set xh=" + xxx.ToString() + " where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'";
                    cmd.ExecuteNonQuery();
                    rnum = rq + getxh(xh + 1,4);
                }
                else
                {
                    cmd.CommandText = "insert into sys_lshglb(xh,bm,lm,rq) values(1,'" + tablename + "','" + lm + "','" + rq + "')";
                    cmd.ExecuteNonQuery();
                    rnum = rq + "0001";
                }
            }
            catch
            {

            }
            conn.Close();
            return rnum;
        }

        /// <summary>
        /// 取得流水号
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="lm">列名</param>
        /// <returns>流水号</returns>
        public static string getlsh(string tablename, string lm, OleDbTransaction tr)
        {
            tablename = tablename.ToLower();
            lm = lm.ToLower();

            OleDbCommand cmd;
            string rq;					//日期
            int num;					//序号
            string rnum = "";
            rq = getrq();

            try
            {
                cmd = new OleDbCommand("select count(*) from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'", tr.Connection);
                cmd.Transaction = tr;
                num = int.Parse(cmd.ExecuteScalar().ToString());
                if (num > 0)
                {
                    int xh;
                    cmd.CommandText = "select xh from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'";
                    xh = int.Parse(cmd.ExecuteScalar().ToString());
                    int xxx = xh + 1;
                    cmd.CommandText = "update sys_lshglb set xh=" + xxx.ToString() + " where bm='" + tablename + "' and lm='" + lm + "' and rq='" + rq + "'";
                    cmd.ExecuteNonQuery();
                    rnum = rq + getxh(xh + 1,4);
                }
                else
                {
                    cmd.CommandText = "insert into sys_lshglb(xh,bm,lm,rq) values(1,'" + tablename + "','" + lm + "','" + rq + "')";
                    cmd.ExecuteNonQuery();
                    rnum = rq + "0001";
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return rnum;

        }




        /// <summary>
        /// 取得36位流水号
        /// </summary>
        /// <param name="tr">外部事务</param>
        /// <param name="talbename">表名</param>
        /// <param name="lm">列名</param>
        /// <returns></returns>
        public static string getlsh36(OleDbTransaction tr, string tablename, string lm)
        {
            tablename = tablename.ToLower();
            if (lm != "X")
                lm = lm.ToLower();

            string rq;//日期
            int num;//序号
            string rnum = "";
            try
            {
                num = Int32.Parse(DataBase.Base_Scalar("select " + OleDbIsNull("count(*)", "0") + " from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "'", tr));
                if (num > 0)
                {
                    string xh;
                    xh = DataBase.Base_Scalar("select " + OleDbIsNull("rq", "'0001'") + " from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and xh=1", tr);
                    if (xh != "0000")
                    {
                        xh = getxh(4, xh);
                        DataBase.Base_cmd("update sys_lshglb set rq='" + xh + "' where bm='" + tablename + "' and lm='" + lm + "' and xh=1", tr);
                    }
                    rnum = xh;
                }
                else
                {
                    DataBase.Base_cmd("insert into sys_lshglb(xh,bm,lm,rq) values(1,'" + tablename + "','" + lm + "','0001')", tr);
                    rnum = "0001";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rnum;
        }


        /// <summary>
        /// 取得36位流水号
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="lm"></param>
        /// <returns></returns>
        public static string getlsh36(string tablename, string lm)
        {
            tablename = tablename.ToLower();
            if (lm != "X")
                lm = lm.ToLower();

            string rq;//'日期
            int num;//序号
            string rnum = "";
            try
            {
                num = Int32.Parse(DataBase.Base_Scalar("select " + OleDbIsNull("count(*)", "0") + " from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "'"));
                if (num > 0)
                {
                    string xh;
                    xh = DataBase.Base_Scalar("select " + OleDbIsNull("rq", "'0001'") + " from sys_lshglb where bm='" + tablename + "' and lm='" + lm + "' and xh=1");
                    if (xh != "0000")
                    {
                        xh = getxh(4, xh);
                        DataBase.Base_cmd("update sys_lshglb set rq='" + xh + "' where bm='" + tablename + "' and lm='" + lm + "' and xh=1");
                    }
                    rnum = xh;
                }
                else
                {
                    DataBase.Base_cmd("insert into sys_lshglb(xh,bm,lm,rq) values(1,'" + tablename + "','" + lm + "','0001')");
                    rnum = "0001";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rnum;
        }
        #endregion

        #region sql对应函数变换

        /// <summary>
        /// 返回sql对应的时间函数
        /// </summary>
        /// <returns></returns>
        public static string OleDbgetdate()
        {
            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "getdate()";
                case "1":
                    return "sysdate";
                case "2":
                    return "date()";
                default:
                    return "getdate()";
            }
        }

        /// <summary>
        /// 返回sql对应isnull的函数
        /// </summary>
        /// <param name="y">字段名</param>
        /// <param name="nvl">转换值</param>
        /// <returns></returns>
        public static string OleDbIsNull(string y, string nvl)
        {
            string NVL = nvl;
            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "isnull(" + y + "," + NVL + ")";
                case "1":
                    return "nvl(" + y + "," + NVL + ")";
                case "2":
                    return "iif(isnull(" + y + ")," + NVL + "," + y + ")";
                default:
                    return "isnull(" + y + "," + NVL + ")";
            }
        }

        /// <summary>
        /// 返回sql对应的字符串转时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string OleDbStrToDate(string date)
        {
            if (date == null || date == "")
                date = DateTime.Today.ToShortDateString();

            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "'" + date + "'";
                case "1":
                    return "to_date('" + date + "','yyyy-mm-dd')";
                case "2":
                    return "datevalue('" + date + "')";
                default:
                    return "'" + date + "'";
            }
        }

        /// <summary>
        /// 返回sql对应的字符串转时间（时分秒）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string OleDbStrToDateTime(string date)
        {
            if (date == null || date == "")
                date = DateTime.Today.ToShortDateString();

            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "'" + date + "'";
                case "1":
                    return "to_date('" + date + "','yyyy-mm-dd hh24:MI:ss')";
                case "2":
                    return "datevalue('" + date + "')";
                default:
                    return "'" + date + "'";
            }
        }

        /// <summary>
        /// 返回年
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string OleDBGetYear(string date)
        {
            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "year(" + date + ")";
                case "1":
                    return "to_char(" + date + ",'yyyy')";
                default:
                    return "year(" + date + ")";
            }
        }

        /// <summary>
        /// 返回月
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string OleDBGetMonth(string date)
        {
            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "month(" + date + ")";
                case "1":
                    return "to_char(" + date + ",'mm')";
                default:
                    return "month(" + date + ")";
            }
        }

        /// <summary>
        /// 字符串转数字
        /// </summary>
        /// <param name="str">字符串字段</param>
        /// <returns></returns>
        public static string OleStrToNUM(string str)
        {
            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "cast(" + str + " as numeric)";
                case "1":
                    return "to_number(" + str + ")";
                default:
                    return "cast(" + str + " as numeric)";
            }
        }

        /// <summary>
        /// 数字转字符串
        /// </summary>
        /// <param name="str">字符串字段</param>
        /// <returns></returns>
        public static string OleNUMToStr(string str)
        {
            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "cast(" + str + " as varchar)";
                case "1":
                    return "to_char(" + str + ")";
                default:
                    return "cast(" + str + " as varchar)";
            }
        }

        /// <summary>
        /// 返回sql中字符串对应的相加符号
        /// </summary>
        /// <returns></returns>
        public static string OleDbStrAdd()
        {
            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "+";
                case "1":
                    return "||";
                case "2":
                    return "+";
                default:
                    return "+";
            }
        }
        public static string OleDbStrSub(string a, string star, string length)
        {
            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "Substring(" + a + "," + star + "," + length + ")";
                case "1":
                    return "Substr(" + a + "," + star + "," + length + ")";
                case "2":
                    return "Substring(" + a + "," + star + "," + length + ")";
                default:
                    return "Substring(" + a + "," + star + "," + length + ")";
            }
        }

        /// <summary>
        /// 增加月
        /// </summary>
        /// <param name="col"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string OleDbAddMonth(string col, int month)
        {
            switch (DataBase.OleDbClass())
            {
                case "0":
                    return "dateadd(mm, " + month.ToString() + ", " + col + ")";
                case "1":
                    return "add_months(" + col + ", " + month.ToString() + ")";
                default:
                    return "dateadd(mm, " + month.ToString() + ", " + col + ")";
            }
        }


        #endregion

        #region 页面权限判断


        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public static bool getqxpd(string userid, string menuid)
        {
            string qx = "";
            qx = DataBase.Base_Scalar("SELECT power FROM SYS_USER where userid='" + userid + "'");
            if (DataBase.Base_Scalar("select " + DataOper.OleDbIsNull("isMust", "'0'") + " from SYS_Menu where menuid='" + menuid + "'").Trim() == "1")
            {
                return false;
            }
            else if (qx == "")
            {
                return true;
            }
            else
            {
                qx = DataOper.getBankTrans(qx);
                if (qx == "")
                {
                    return true;
                }
                else
                {
                    string aaaa = qx.Substring(0, qx.IndexOf("#"));
                    if (qx.IndexOf("#" + menuid + "#") > -1 | menuid == aaaa)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        /// <summary>
        /// 页面权限判断
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool getqxpdy(string userid, string path)
        {
            string[] p = DataOper.Split(path, "/");
            string pathh = p[p.Length - 2] + "/" + p[p.Length - 1];
            string qx = "";
            string menuid = DataBase.Base_Scalar("select menuid from SYS_Menu where hlink like '%" + pathh + "'");
            qx = DataOper.getBankTrans(DataBase.Base_Scalar("SELECT power FROM SYS_USER where userid='" + userid + "'"));
            if (qx == "")
                return true;
            string aaaa = qx.Substring(0, qx.IndexOf("#"));
            if (qx.IndexOf("#" + menuid + "#") > -1 | menuid == aaaa)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 返回菜单名
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="state">0 本级菜单，1上级菜单</param>
        /// <returns></returns>
        public static string retMenuTitle(string path, string state)
        {
            string[] p = DataOper.Split(path, "/");
            string pathh = p[p.Length - 2] + "/" + p[p.Length - 1];
            string qx = "";
            string menuid = DataBase.Base_Scalar("select menuid from SYS_Menu where hlink like '%" + pathh + "'");

            string title = "";

            if (state == "0")
                title = DataBase.Base_Scalar("select menuname from sys_menu where menuid='" + menuid + "'");
            else
            {
                title = DataBase.Base_Scalar("select menuname from sys_menu where menuid=(select main_id from sys_menu where menuid='" + menuid + "')");
            }

            return title;

        }


        #endregion

        #region 其他

        /// <summary>
        /// 树根名称
        /// </summary>
        /// <returns></returns>
        public static string treetitle()
        {
            return ConfigurationManager.AppSettings["BMTree"];
        }


        /// <summary>
        /// 默认密码判断
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static bool remm(string userid)
        {
            if (DataBase.Base_Scalar("select password from SYS_USER where userid='" + userid + "'") == DataOper.encryptsha1("666666"))
                return true;
            else
                return false;
        }
        #endregion

        /// <summary>
        /// 返回日期是当月第几周
        /// </summary>
        /// <param name="day">当前日期</param>
        /// <returns></returns>
        public static int WeekOfMonth(DateTime day)
        {
            int ret;
            //当月的一号是周几
            int dy1 = int.Parse(DateTime.Parse(day.Year.ToString() + "-" + day.Month.ToString() + "-" + "1").DayOfWeek.ToString("d"));
            int dyz = int.Parse(DateTime.Parse(day.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).DayOfWeek.ToString("d"));//本月最后一天周几
            if (dy1 == 0)
                dy1 = 7;

            if (dyz == 0)
                dyz = 7;


            int dr = day.Day;//当前天
            int yt = DateTime.Parse(day.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).Day;//月末天
            if (dr <= 7 - dy1 + 1 && dy1 != 1)
                ret = 0;
            else if (dr <= 7 && dy1 == 1)
                ret = 1;
            else if ((dr > yt - dyz && dyz != 7) || (dyz == 7 && dr > yt - 7))
                ret = getWeekCount(day);
            else
            {
                int ss = dr - (7 - dy1 + 1);
                if (ss % 7 == 0)
                    ret = ss / 7;
                else
                    ret = ss / 7 + 1;
            }


            //return ret;//返回本周

            //返回下一周
            if (ret + 1 > getWeekCount(day))
                return 0;
            else
                return ret + 1;


            //int daysOfWeek;
            //if (dy1 >= 6)
            //{
            //    daysOfWeek = int.Parse(day.DayOfWeek.ToString("d")) + 1 + (7 - dy1);
            //}
            //else
            //    daysOfWeek = int.Parse(day.DayOfWeek.ToString("d")) + 1;
            //if ((day.AddDays(0 - daysOfWeek).Month < day.Month) || (day.AddDays(0 - daysOfWeek).Month == 12 && day.Month == 1))
            //    return 1;
            //else if ((day.AddDays(0 - daysOfWeek - 7).Month < day.Month) || (day.AddDays(0 - daysOfWeek - 7).Month == 12 && day.Month == 1))
            //    return 2;
            //else if ((day.AddDays(0 - daysOfWeek - 14).Month < day.Month) || (day.AddDays(0 - daysOfWeek - 14).Month == 12 && day.Month == 1))
            //    return 3;
            //else if ((day.AddDays(0 - daysOfWeek - 21).Month < day.Month) || (day.AddDays(0 - daysOfWeek - 21).Month == 12 && day.Month == 1))
            //    return 4;
            //else
            //{
            //    if (dyz >= 5)
            //        return 5;
            //    else
            //        return 0;
            //}

        }

        /// <summary>
        /// 返回这个月共几周
        /// </summary>
        /// <param name="day">选择月最后一天</param>
        /// <returns></returns>
        public static int getWeekCount(DateTime day)
        {//DateTime.Parse(DateTime.Parse("2009-09-01").AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1)).ToString()
            int count = 0;
            int dy1 = int.Parse(DateTime.Parse(day.ToString("yyyy-MM-01")).DayOfWeek.ToString("d"));//1日星期
            int dyz = int.Parse(DateTime.Parse(day.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).DayOfWeek.ToString("d"));//月末星期
            if (dy1 == 0)
                dy1 = 7;

            if (dyz == 0)
                dyz = 7;

            int yt = DateTime.Parse(day.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).Day;
            if (dy1 != 1)
            {
                count = (yt - dyz - (7 - dy1 + 1)) / 7 + 1;
            }
            else
            {
                count = (yt - dyz) / 7 + 1;
            }

            //if (dyz == 7)
            //    count += 1;

            return count;


            //int daysOfWeek;
            //if (dy1 >= 6)
            //{
            //    daysOfWeek = int.Parse(day.DayOfWeek.ToString("d")) + 1 + (7 - dy1);
            //}
            //else
            //    daysOfWeek = int.Parse(day.DayOfWeek.ToString("d")) + 1;

            //if ((day.AddDays(0 - daysOfWeek - 21).Month < day.Month) || (day.AddDays(0 - daysOfWeek - 21).Month == 12 && day.Month == 1))
            //    return 4;
            //else
            //{
            //    if (dyz == 5)
            //        return 5;
            //    else
            //        return 4;
            //}
        }

        public static void addlog(string userid, string ip, string cz)
        {
            DataBase.Base_cmd("insert into sys_log (logid,username,ip,cz,czsj) values ('" + getlsh("sys_log", "logid") + "','" + setTrueString(userid) + "','" + setTrueString(ip) + "','" + setTrueString(cz) + "'," + OleDbgetdate() + ")");
        }

        public static void addlog(string userid, string ip, string cz, OleDbTransaction tr)
        {
            try
            {
                DataBase.Base_cmd("insert into sys_log (logid,username,ip,cz,czsj) values ('" + getlsh("sys_log", "logid") + "','" + setTrueString(userid) + "','" + setTrueString(ip) + "','" + setTrueString(cz) + "'," + OleDbgetdate() + ")", tr);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string DateToString(string zd)
        {
            if (DataBase.OleDbClass() == "0")
            {
                return " replace(convert(varchar," + zd + ",102),'.','-')";
            }
            if (DataBase.OleDbClass() == "1")
            {
                return " to_char(" + zd + ",'yyyy-MM-dd')";
            }
            return "";
        }
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string getIP()
        {
            //HttpRequest Request;
            //string ip = Request.ServerVariables("HTTP_X_FORWARDED_FOR");
            //if (ip == "")
            //    ip = Request.ServerVariables("REMOTE_ADDR");

            //return ip;

            string hostName = Dns.GetHostName();
            IPHostEntry hi = Dns.GetHostEntry(hostName);
            string ip = hi.AddressList[0].ToString();

            return ip;
        }
    }
}
