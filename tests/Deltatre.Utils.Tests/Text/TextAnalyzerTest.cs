using Deltatre.Utils.Text;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deltatre.Utils.Tests.Text
{
	[TestFixture]
	public class TextAnalyzerTest
	{
		[Test]
		public void Should_be_possible_to_Count_Words_for_Afrikaans_Text()
		{
			var target = CreateTarget();

			var text =
				"Ons sportliefhebbers eis ongeëwenaarde toegang enige tyd, enige plek en op 'n aantal toestelle. Om saam met die span te werk, gee ons die kundigheid en bewese tegnologie wat ons nodig het om so 'n ambisieuse nuwe diens bekend te stel.";

			var result = target.CountWords(text, "af-ZA");

			Assert.AreEqual(41, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Afrikaans_Text()
		{
			var target = CreateTarget();

			var text =
				"Ons sportliefhebbers eis ongeëwenaarde toegang enige tyd, enige plek en op 'n aantal toestelle. Om saam met die span te werk, gee ons die kundigheid en bewese tegnologie wat ons nodig het om so 'n ambisieuse nuwe diens bekend te stel.";

			var result = target.CountCharacters(text, "af-ZA");

			Assert.AreEqual(234, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Albanian_Text()
		{
			var target = CreateTarget();

			var text =
				"Tifozët tanë të sportit kërkojnë qasje të pakrahasueshme në çdo kohë, kudo dhe në një numër pajisjesh. Puna me ekipin na jep ekspertizë dhe teknologji të dëshmuar që na duhet për të filluar një shërbim të ri kaq ambicioz.";

			var result = target.CountWords(text, "sq-AL");

			Assert.AreEqual(39, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Albanian_Text()
		{
			var target = CreateTarget();

			var text =
				"Tifozët tanë të sportit kërkojnë qasje të pakrahasueshme në çdo kohë, kudo dhe në një numër pajisjesh. Puna me ekipin na jep ekspertizë dhe teknologji të dëshmuar që na duhet për të filluar një shërbim të ri kaq ambicioz.";

			var result = target.CountCharacters(text, "sq-AL");

			Assert.AreEqual(221, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Amharic_Text()
		{
			var target = CreateTarget();

			var text = "የእኛ የስፖርት አድናቂዎች በማንኛውም ሰዓት ፣ በማንኛውም ቦታ እና በበርካታ መሳሪያዎች ላይ ደህንነቱ ያልተጠበቀ መዳረሻ ይጠይቃሉ። ከቡድኑ ጋር አብሮ መሥራት እንደዚህ ዓይነቱን ምኞት አዲስ አገልግሎት ለመጀመር የሚያስፈልገንን ሙያዊ እና የተረጋገጠ ቴክኖሎጂ ይሰጠናል።";

			var result = target.CountWords(text, "am-ET");

			Assert.AreEqual(32, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Amharic_Text()
		{
			var target = CreateTarget();

			var text = "የእኛ የስፖርት አድናቂዎች በማንኛውም ሰዓት ፣ በማንኛውም ቦታ እና በበርካታ መሳሪያዎች ላይ ደህንነቱ ያልተጠበቀ መዳረሻ ይጠይቃሉ። ከቡድኑ ጋር አብሮ መሥራት እንደዚህ ዓይነቱን ምኞት አዲስ አገልግሎት ለመጀመር የሚያስፈልገንን ሙያዊ እና የተረጋገጠ ቴክኖሎጂ ይሰጠናል።";

			var result = target.CountCharacters(text, "am-ET");

			Assert.AreEqual(170, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Armenian_Text()
		{
			var target = CreateTarget();

			var text = "Մեր մարզասերները պահանջում են անզուգական մուտք ցանկացած պահի, ցանկացած վայրում և մի շարք սարքերով: Թիմի հետ աշխատելը մեզ տալիս է փորձ և ապացուցված տեխնոլոգիա, որը մեզ անհրաժեշտ է նման հավակնոտ նոր ծառայություն սկսելու համար:";

			var result = target.CountWords(text, "hy-AM");

			Assert.AreEqual(34, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Armenian_Text()
		{
			var target = CreateTarget();

			var text = "Մեր մարզասերները պահանջում են անզուգական մուտք ցանկացած պահի, ցանկացած վայրում և մի շարք սարքերով: Թիմի հետ աշխատելը մեզ տալիս է փորձ և ապացուցված տեխնոլոգիա, որը մեզ անհրաժեշտ է նման հավակնոտ նոր ծառայություն սկսելու համար:";

			var result = target.CountCharacters(text, "hy-AM");

			Assert.AreEqual(224, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Azeri_Text()
		{
			var target = CreateTarget();

			var text = "İdman həvəskarlarımız hər yerdə, hər yerdə və bir sıra cihazlarda rakursuz çıxış tələb edirlər. Komanda ilə işləmək, belə bir iddialı yeni bir xidmətə başlamağımız üçün lazım olan təcrübə və sübut edilmiş texnologiya verir.";

			var result = target.CountWords(text, "az-Cyrl-AZ");

			Assert.AreEqual(33, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Azeri_Text()
		{
			var target = CreateTarget();

			var text = "İdman həvəskarlarımız hər yerdə, hər yerdə və bir sıra cihazlarda rakursuz çıxış tələb edirlər. Komanda ilə işləmək, belə bir iddialı yeni bir xidmətə başlamağımız üçün lazım olan təcrübə və sübut edilmiş texnologiya verir.";

			var result = target.CountCharacters(text, "az-Cyrl-AZ");

			Assert.AreEqual(223, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_American_Text()
		{
			var target = CreateTarget();

			var text =
				"Forty-five seasons into his coaching career, Bill Belichick appears to be having more fun than ever. He's taken charge of a deep, malleable, veteran defense built on the same 3-4 principles that he first taught the New York Giants as a coordinator in the 1980s, complete with veteran linebackers and a diverse secondary. The 65-year-old's top two defensive assistants are his son, Steve, and one of his favorite former players, Jerod Mayo, both of whom are roughly half his age. In Bill Belichick's world, there's nothing more fun than beating the organization that once fired you (even if, you know, technically that was a different team) in a driving rainstorm under grey skies. The uglier, the better.";

			var result = target.CountWords(text, "en-US");

			Assert.AreEqual(117, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_American_Text()
		{
			var target = CreateTarget();

			var text =
				"Forty-five seasons into his coaching career, Bill Belichick appears to be having more fun than ever. He's taken charge of a deep, malleable, veteran defense built on the same 3-4 principles that he first taught the New York Giants as a coordinator in the 1980s, complete with veteran linebackers and a diverse secondary. The 65-year-old's top two defensive assistants are his son, Steve, and one of his favorite former players, Jerod Mayo, both of whom are roughly half his age. In Bill Belichick's world, there's nothing more fun than beating the organization that once fired you (even if, you know, technically that was a different team) in a driving rainstorm under grey skies. The uglier, the better.";

			var result = target.CountCharacters(text, "en-US");

			Assert.AreEqual(704, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Basque_Text()
		{
			var target = CreateTarget();

			var text =
				"Gure kirol zaleek paregabeko sarbidea eskatzen dute noiznahi, edozein lekutan eta hainbat gailutan. Taldearekin lan egiteak hain zerbitzu handinahi berria abian jartzeko behar dugun eskarmentua eta frogatutako teknologia eskaintzen digu";

			var result = target.CountWords(text, "eu-ES");

			Assert.AreEqual(30, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Basque_Text()
		{
			var target = CreateTarget();

			var text =
				"Gure kirol zaleek paregabeko sarbidea eskatzen dute noiznahi, edozein lekutan eta hainbat gailutan. Taldearekin lan egiteak hain zerbitzu handinahi berria abian jartzeko behar dugun eskarmentua eta frogatutako teknologia eskaintzen digu";

			var result = target.CountCharacters(text, "eu-ES");

			Assert.AreEqual(236, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Bengali_Text()
		{
			var target = CreateTarget();

			var text =
				"আমাদের ক্রীড়া অনুরাগীরা যে কোনও সময়, যে কোনও জায়গায় এবং বেশ কয়েকটি ডিভাইসে অবিচ্ছিন্ন অ্যাক্সেসের দাবি করে। দলের সাথে কাজ করা আমাদের এমন উচ্চাভিলাষী নতুন পরিষেবা চালু করার জন্য প্রয়োজনীয় দক্ষতা এবং প্রমাণিত প্রযুক্তি দেয়।";

			var result = target.CountWords(text, "bn-IN");

			Assert.AreEqual(35, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Bengali_Text()
		{
			var target = CreateTarget();

			var text =
				"আমাদের ক্রীড়া অনুরাগীরা যে কোনও সময়, যে কোনও জায়গায় এবং বেশ কয়েকটি ডিভাইসে অবিচ্ছিন্ন অ্যাক্সেসের দাবি করে। দলের সাথে কাজ করা আমাদের এমন উচ্চাভিলাষী নতুন পরিষেবা চালু করার জন্য প্রয়োজনীয় দক্ষতা এবং প্রমাণিত প্রযুক্তি দেয়।";

			var result = target.CountCharacters(text, "bn-IN");

			Assert.AreEqual(229, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Belarusian_Text()
		{
			var target = CreateTarget();

			var text =
				"Нашы аматары спорту патрабуюць неперасягненага доступу ў любы час і ў любым месцы і на некалькіх прыладах. Праца з камандай дае нам вопыт і правераныя тэхналогіі, неабходныя для запуску такой амбіцыйнай новай паслугі.";

			var result = target.CountWords(text, "be-BY");

			Assert.AreEqual(33, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Belarusian_Text()
		{
			var target = CreateTarget();

			var text =
				"Нашы аматары спорту патрабуюць неперасягненага доступу ў любы час і ў любым месцы і на некалькіх прыладах. Праца з камандай дае нам вопыт і правераныя тэхналогіі, неабходныя для запуску такой амбіцыйнай новай паслугі.";

			var result = target.CountCharacters(text, "be-BY");

			Assert.AreEqual(217, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Bulgarian_Text()
		{
			var target = CreateTarget();

			var text =
				"Нашите фенове на спорта изискват ненадминат достъп по всяко време, навсякъде и на редица устройства. Работата с екипа ни дава опит и доказана технология, от които се нуждаем, за да стартираме такава амбициозна нова услуга.";

			var result = target.CountWords(text, "bg-BG");

			Assert.AreEqual(35, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Bulgarian_Text()
		{
			var target = CreateTarget();

			var text =
				"Нашите фенове на спорта изискват ненадминат достъп по всяко време, навсякъде и на редица устройства. Работата с екипа ни дава опит и доказана технология, от които се нуждаем, за да стартираме такава амбициозна нова услуга.";

			var result = target.CountCharacters(text, "bg-BG");

			Assert.AreEqual(222, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Italian_Text()
		{
			var target = CreateTarget();

			var text = "Stop all’aumento della cedolare secca sugli affitti. Si ferma la stretta sulla Flat tax per i lavoratori autonomi. Nuove risorse per il piano Impresa 4.0 ma per investimenti che siano sostenibili dal punto di vista ambientale. Il vertice di maggioranza di ieri ha corretto tre punti del disegno di legge di Bilancio, approvato «salvo intese» dal consiglio dei ministri due settimane fa ma non ancora arrivato in Parlamento. Oggi una nuova riunione dovrebbe limare gli ultimi punti del testo. Poi comincerà l’esame del Parlamento. E nuove modifiche sono possibili.";

			var result = target.CountWords(text, "en-US");

			Assert.AreEqual(89, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Italian_Text()
		{
			var target = CreateTarget();

			var text = "Stop all’aumento della cedolare secca sugli affitti. Si ferma la stretta sulla Flat tax per i lavoratori autonomi. Nuove risorse per il piano Impresa 4.0 ma per investimenti che siano sostenibili dal punto di vista ambientale. Il vertice di maggioranza di ieri ha corretto tre punti del disegno di legge di Bilancio, approvato «salvo intese» dal consiglio dei ministri due settimane fa ma non ancora arrivato in Parlamento. Oggi una nuova riunione dovrebbe limare gli ultimi punti del testo. Poi comincerà l’esame del Parlamento. E nuove modifiche sono possibili.";

			var result = target.CountCharacters(text, "en-US");

			Assert.AreEqual(563, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Arabic_Text()
		{
			var target = CreateTarget();

			var text = "رئاسة الحكومة، وذلك بعد أن وصل إلى ما سماه طريقا مسدودا. وقدم الحريري استقالته للرئيس ميشال عون.";

			var result = target.CountWords(text, "ar-SA");

			Assert.AreEqual(17, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Arabic_Text()
		{
			var target = CreateTarget();

			var text = "رئاسة الحكومة، وذلك بعد أن وصل إلى ما سماه طريقا مسدودا. وقدم الحريري استقالته للرئيس ميشال عون.";

			var result = target.CountCharacters(text, "ar-SA");

			Assert.AreEqual(96, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Russian_Text()
		{
			var target = CreateTarget();

			var text = "Рассказываем об американцах, которые оставили заметный след в еврокубках.";

			var result = target.CountWords(text, "ru-RU");

			Assert.AreEqual(9, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Russian_Text()
		{
			var target = CreateTarget();

			var text = "Рассказываем об американцах, которые оставили заметный след в еврокубках.";

			var result = target.CountCharacters(text, "ru-RU");

			Assert.AreEqual(73, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Urdu_Text()
		{
			var target = CreateTarget();

			var text = "ہیلو آپ سب کیسے ٹھیک ہیں";

			var result = target.CountWords(text, "ur-PK");

			Assert.AreEqual(6, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Urdu_Text()
		{
			var target = CreateTarget();

			var text = "ہیلو آپ سب کیسے ٹھیک ہیں";

			var result = target.CountCharacters(text, "ur-PK");

			Assert.AreEqual(24, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Chinese_Simplified_Text()
		{
			var target = CreateTarget();

			var text = "淘宝为第三方交易平台及互联网信息服务提供者，淘宝（含网站、客户端等）所展示的商品/服务的标题、价格、详情等信息内容系由店铺经营者发布，其真实性、准确性和合法性均由店铺经营者负责。淘宝提醒用户购买商品/服务前注意谨慎核实。如用户对商品/服务的标题、价格、详情等任何信息有任何疑问的，请在购买前通过阿里旺旺与店铺经营者沟通确认；淘宝存在海量店铺，如用户发现店铺内有任何违法/侵权信息，请立即向淘宝举报并提供有效线索。";

			var result = target.CountWords(text, "zh-CN");

			Assert.AreEqual(206, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Chinese_Simplified_Text()
		{
			var target = CreateTarget();

			var text = "淘宝为第三方交易平台及互联网信息服务提供者，淘宝（含网站、客户端等）所展示的商品/服务的标题、价格、详情等信息内容系由店铺经营者发布，其真实性、准确性和合法性均由店铺经营者负责。淘宝提醒用户购买商品/服务前注意谨慎核实。如用户对商品/服务的标题、价格、详情等任何信息有任何疑问的，请在购买前通过阿里旺旺与店铺经营者沟通确认；淘宝存在海量店铺，如用户发现店铺内有任何违法/侵权信息，请立即向淘宝举报并提供有效线索。";

			var result = target.CountCharacters(text, "zh-CN");

			Assert.AreEqual(206, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Chinese_Traditional_Text()
		{
			var target = CreateTarget();

			var text = "我們的體育迷要求隨時隨地在許多設備上獲得無與倫比的訪問權限。 與團隊合作為我們提供了開展如此雄心勃勃的新服務所需的專業知識和成熟技術。";

			var result = target.CountWords(text, "zh-HK");

			Assert.AreEqual(66, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Chinese_Traditional_Text()
		{
			var target = CreateTarget();

			var text = "我們的體育迷要求隨時隨地在許多設備上獲得無與倫比的訪問權限。 與團隊合作為我們提供了開展如此雄心勃勃的新服務所需的專業知識和成熟技術。";

			var result = target.CountCharacters(text, "zh-HK");

			Assert.AreEqual(67, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Korean_Text()
		{
			var target = CreateTarget();

			var text = "우리의 스포츠 팬들은 언제 어디서나 많은 장치에서 최고의 액세스를 요구합니다. 팀과 협력하면 야심 찬 새 서비스를 시작하는 데 필요한 전문성과 입증 된 기술이 제공됩니다.";

			var result = target.CountWords(text, "ko-KR");

			Assert.AreEqual(72, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Korean_Text()
		{
			var target = CreateTarget();

			var text = "우리의 스포츠 팬들은 언제 어디서나 많은 장치에서 최고의 액세스를 요구합니다. 팀과 협력하면 야심 찬 새 서비스를 시작하는 데 필요한 전문성과 입증 된 기술이 제공됩니다.";

			var result = target.CountCharacters(text, "ko-KR");

			Assert.AreEqual(95, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Greek_Text()
		{
			var target = CreateTarget();

			var text = "Οι φίλαθλοι μας απαιτούν απαράμιλλη πρόσβαση οποιαδήποτε στιγμή, οπουδήποτε και σε πολλές συσκευές. Η συνεργασία με την ομάδα μας προσφέρει την τεχνογνωσία και την αποδεδειγμένη τεχνολογία που χρειαζόμαστε για να ξεκινήσουμε μια τόσο φιλόδοξη νέα υπηρεσία.";

			var result = target.CountWords(text, "el-GR");

			Assert.AreEqual(36, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Greek_Text()
		{
			var target = CreateTarget();

			var text = "Οι φίλαθλοι μας απαιτούν απαράμιλλη πρόσβαση οποιαδήποτε στιγμή, οπουδήποτε και σε πολλές συσκευές. Η συνεργασία με την ομάδα μας προσφέρει την τεχνογνωσία και την αποδεδειγμένη τεχνολογία που χρειαζόμαστε για να ξεκινήσουμε μια τόσο φιλόδοξη νέα υπηρεσία.";

			var result = target.CountCharacters(text, "el-GR");

			Assert.AreEqual(256, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Hebrew_Text()
		{
			var target = CreateTarget();

			var text = "אוהדי הספורט שלנו דורשים גישה ללא תחרות בכל עת ובכל מקום ובמספר מכשירים. העבודה עם הצוות מעניקה לנו את המומחיות והטכנולוגיה המוכחת הדרושה לנו כדי להשיק שירות חדש ושאפתני כזה.";

			var result = target.CountWords(text, "he-IL");

			Assert.AreEqual(30, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Hebrew_Text()
		{
			var target = CreateTarget();

			var text = "אוהדי הספורט שלנו דורשים גישה ללא תחרות בכל עת ובכל מקום ובמספר מכשירים. העבודה עם הצוות מעניקה לנו את המומחיות והטכנולוגיה המוכחת הדרושה לנו כדי להשיק שירות חדש ושאפתני כזה.";

			var result = target.CountCharacters(text, "he-IL");

			Assert.AreEqual(174, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Persian_Text()
		{
			var target = CreateTarget();

			var text = "طرفداران ورزش ما خواستار دسترسی بی نظیر در هر زمان ، هر مکان و تعدادی دستگاه هستند. همکاری با تیم به ما مهارت و فناوری اثبات شده ای را برای راه اندازی چنین سرویس جدید جاه طلبانه می دهد.";

			var result = target.CountWords(text, "fa-IR");

			Assert.AreEqual(39, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Persian_Text()
		{
			var target = CreateTarget();

			var text = "طرفداران ورزش ما خواستار دسترسی بی نظیر در هر زمان ، هر مکان و تعدادی دستگاه هستند. همکاری با تیم به ما مهارت و فناوری اثبات شده ای را برای راه اندازی چنین سرویس جدید جاه طلبانه می دهد.";

			var result = target.CountCharacters(text, "fa-IR");

			Assert.AreEqual(185, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Japanese_Text()
		{
			var target = CreateTarget();

			var text =
				"私たちのスポーツファンは、いつでも、どこでも、多くのデバイスで比類のないアクセスを求めています。チームと協力することで、このような野心的な新しいサービスを立ち上げるために必要な専門知識と実績のある技術を得ることができます。";

			var result = target.CountWords(text, "ja-JP");

			Assert.AreEqual(111, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Japanese_Text()
		{
			var target = CreateTarget();

			var text =
				"私たちのスポーツファンは、いつでも、どこでも、多くのデバイスで比類のないアクセスを求めています。チームと協力することで、このような野心的な新しいサービスを立ち上げるために必要な専門知識と実績のある技術を得ることができます。";

			var result = target.CountCharacters(text, "ja-JP");

			Assert.AreEqual(111, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Gujarati_Text()
		{
			var target = CreateTarget();

			var text =
				"અમારા રમત પ્રશંસકો ગમે ત્યારે, ગમે ત્યાં અને સંખ્યાબંધ ઉપકરણો પર અજોડ પ્રવેશની માંગ કરે છે. ટીમ સાથે કામ કરવાથી અમને આવી મહત્વાકાંક્ષી નવી સેવા શરૂ કરવાની આવશ્યકતા અને સાબિત તકનીક મળે છે.";

			var result = target.CountWords(text, "gu-IN");

			Assert.AreEqual(33, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Gujarati_Text()
		{
			var target = CreateTarget();

			var text =
				"અમારા રમત પ્રશંસકો ગમે ત્યારે, ગમે ત્યાં અને સંખ્યાબંધ ઉપકરણો પર અજોડ પ્રવેશની માંગ કરે છે. ટીમ સાથે કામ કરવાથી અમને આવી મહત્વાકાંક્ષી નવી સેવા શરૂ કરવાની આવશ્યકતા અને સાબિત તકનીક મળે છે.";

			var result = target.CountCharacters(text, "gu-IN");

			Assert.AreEqual(187, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Hindi_Text()
		{
			var target = CreateTarget();

			var text =
				"हमारे खेल प्रशंसक कभी भी, कहीं भी और कई उपकरणों पर बेजोड़ एक्सेस की मांग करते हैं। टीम के साथ काम करने से हमें विशेषज्ञता और सिद्ध तकनीक मिलती है, हमें इस तरह की महत्वाकांक्षी नई सेवा शुरू करने की जरूरत है।";

			var result = target.CountWords(text, "hi-IN");

			Assert.AreEqual(42, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Hindi_Text()
		{
			var target = CreateTarget();

			var text =
				"हमारे खेल प्रशंसक कभी भी, कहीं भी और कई उपकरणों पर बेजोड़ एक्सेस की मांग करते हैं। टीम के साथ काम करने से हमें विशेषज्ञता और सिद्ध तकनीक मिलती है, हमें इस तरह की महत्वाकांक्षी नई सेवा शुरू करने की जरूरत है।";

			var result = target.CountCharacters(text, "hi-IN");

			Assert.AreEqual(206, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Punjabi_Text()
		{
			var target = CreateTarget();

			var text =
				"ਸਾਡੇ ਖੇਡ ਪ੍ਰਸ਼ੰਸਕ ਕਦੇ ਵੀ, ਕਿਤੇ ਵੀ ਅਤੇ ਕਈ ਡਿਵਾਈਸਿਸਾਂ 'ਤੇ ਬੇਜੋੜ ਪਹੁੰਚ ਦੀ ਮੰਗ ਕਰਦੇ ਹਨ. ਟੀਮ ਨਾਲ ਕੰਮ ਕਰਨਾ ਸਾਨੂੰ ਮੁਹਾਰਤ ਅਤੇ ਸਾਬਤ ਤਕਨਾਲੋਜੀ ਦਿੰਦਾ ਹੈ ਜਿਸਦੀ ਸਾਨੂੰ ਅਜਿਹੀ ਮਹੱਤਵਪੂਰਣ ਨਵੀਂ ਸੇਵਾ ਸ਼ੁਰੂ ਕਰਨ ਦੀ ਜ਼ਰੂਰਤ ਹੈ.";

			var result = target.CountWords(text, "pa-IN");

			Assert.AreEqual(39, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Punjabi_Text()
		{
			var target = CreateTarget();

			var text =
				"ਸਾਡੇ ਖੇਡ ਪ੍ਰਸ਼ੰਸਕ ਕਦੇ ਵੀ, ਕਿਤੇ ਵੀ ਅਤੇ ਕਈ ਡਿਵਾਈਸਿਸਾਂ 'ਤੇ ਬੇਜੋੜ ਪਹੁੰਚ ਦੀ ਮੰਗ ਕਰਦੇ ਹਨ. ਟੀਮ ਨਾਲ ਕੰਮ ਕਰਨਾ ਸਾਨੂੰ ਮੁਹਾਰਤ ਅਤੇ ਸਾਬਤ ਤਕਨਾਲੋਜੀ ਦਿੰਦਾ ਹੈ ਜਿਸਦੀ ਸਾਨੂੰ ਅਜਿਹੀ ਮਹੱਤਵਪੂਰਣ ਨਵੀਂ ਸੇਵਾ ਸ਼ੁਰੂ ਕਰਨ ਦੀ ਜ਼ਰੂਰਤ ਹੈ.";

			var result = target.CountCharacters(text, "pa-IN");

			Assert.AreEqual(202, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Thai_Text()
		{
			var target = CreateTarget();

			var text =
				"แฟนกีฬาของเราต้องการการเข้าถึงที่ยอดเยี่ยมทุกที่ทุกเวลาและบนอุปกรณ์จำนวนมาก การทำงานกับทีมทำให้เรามีความเชี่ยวชาญและเทคโนโลยีที่พิสูจน์แล้วว่าเราจำเป็นต้องเปิดตัวบริการใหม่ที่มีความทะเยอทะยาน";

			var result = target.CountWords(text, "th-TH");

			Assert.AreEqual(2, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Thai_Text()
		{
			var target = CreateTarget();

			var text =
				"แฟนกีฬาของเราต้องการการเข้าถึงที่ยอดเยี่ยมทุกที่ทุกเวลาและบนอุปกรณ์จำนวนมาก การทำงานกับทีมทำให้เรามีความเชี่ยวชาญและเทคโนโลยีที่พิสูจน์แล้วว่าเราจำเป็นต้องเปิดตัวบริการใหม่ที่มีความทะเยอทะยาน";

			var result = target.CountCharacters(text, "th-TH");

			Assert.AreEqual(191, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Turkish_Text()
		{
			var target = CreateTarget();

			var text =
				"Sporseverler her zaman, her yerde ve çeşitli cihazlarda rakipsiz erişim talep ediyor. Ekip ile çalışmak bize bu kadar iddialı ve yeni bir servis başlatmak için ihtiyaç duyduğumuz uzmanlığı ve kanıtlanmış teknolojiyi sunuyor.";

			var result = target.CountWords(text, "tr-TR");

			Assert.AreEqual(32, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Turkish_Text()
		{
			var target = CreateTarget();

			var text =
				"Sporseverler her zaman, her yerde ve çeşitli cihazlarda rakipsiz erişim talep ediyor. Ekip ile çalışmak bize bu kadar iddialı ve yeni bir servis başlatmak için ihtiyaç duyduğumuz uzmanlığı ve kanıtlanmış teknolojiyi sunuyor.";

			var result = target.CountCharacters(text, "tr-TR");

			Assert.AreEqual(224, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Mongolian_Cyrillic_Text()
		{
			var target = CreateTarget();

			var text =
				"Манай спортын фэнүүд хэзээ ч, хаанаас ч, хэд хэдэн төхөөрөмжөөс давтагдашгүй хандалтыг шаарддаг. Багтай хамтарч ажиллах нь бидэнд маш том шинэ үйлчилгээг эхлүүлэхэд шаардлагатай туршлага, батлагдсан технологийг өгдөг.";

			var result = target.CountWords(text, "mn-MN");

			Assert.AreEqual(28, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Mongolian_Cyrillic_Text()
		{
			var target = CreateTarget();

			var text =
				"Манай спортын фэнүүд хэзээ ч, хаанаас ч, хэд хэдэн төхөөрөмжөөс давтагдашгүй хандалтыг шаарддаг. Багтай хамтарч ажиллах нь бидэнд маш том шинэ үйлчилгээг эхлүүлэхэд шаардлагатай туршлага, батлагдсан технологийг өгдөг.";

			var result = target.CountCharacters(text, "mn-MN");

			Assert.AreEqual(217, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Nepali_Text()
		{
			var target = CreateTarget();

			var text =
				"हाम्रो खेल फ्यानहरूले कुनै पनि समय, कहिँ पनि र धेरै उपकरणहरूमा अतुलनीय पहुँचको माग गर्दछ। टोलीसँग काम गर्दा हामीलाई विशेषज्ञता र प्रमाणित टेक्नोलोजी दिन्छ जुन हामीले यस्तो महत्वाकांक्षी नयाँ सेवा सुरू गर्न आवश्यक छ।";

			var result = target.CountWords(text, "ne-NP");

			Assert.AreEqual(34, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Nepali_Text()
		{
			var target = CreateTarget();

			var text =
				"हाम्रो खेल फ्यानहरूले कुनै पनि समय, कहिँ पनि र धेरै उपकरणहरूमा अतुलनीय पहुँचको माग गर्दछ। टोलीसँग काम गर्दा हामीलाई विशेषज्ञता र प्रमाणित टेक्नोलोजी दिन्छ जुन हामीले यस्तो महत्वाकांक्षी नयाँ सेवा सुरू गर्न आवश्यक छ।";

			var result = target.CountCharacters(text, "ne-NP");

			Assert.AreEqual(215, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Macedonian_Text()
		{
			var target = CreateTarget();

			var text =
				"Нашите fansубители на спортот бараат неповторлив пристап во секое време, насекаде и на голем број уреди. Работата со тимот ни дава експертиза и докажана технологија што ни е потребна за да започнеме ваква амбициозна нова услуга.";

			var result = target.CountWords(text, "mk-MK");

			Assert.AreEqual(36, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Macedonian_Text()
		{
			var target = CreateTarget();

			var text =
				"Нашите fansубители на спортот бараат неповторлив пристап во секое време, насекаде и на голем број уреди. Работата со тимот ни дава експертиза и докажана технологија што ни е потребна за да започнеме ваква амбициозна нова услуга.";

			var result = target.CountCharacters(text, "mk-MK");

			Assert.AreEqual(228, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_French_Text()
		{
			var target = CreateTarget();

			var text =
				"Nos fans de sport exigent un accès inégalé à tout moment, n'importe où et sur de nombreux appareils. Travailler avec l'équipe nous donne l'expertise et la technologie éprouvée dont nous avons besoin pour lancer un nouveau service aussi ambitieux.";

			var result = target.CountWords(text, "fr-FR");

			Assert.AreEqual(39, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_French_Text()
		{
			var target = CreateTarget();

			var text =
				"Nos fans de sport exigent un accès inégalé à tout moment, n'importe où et sur de nombreux appareils. Travailler avec l'équipe nous donne l'expertise et la technologie éprouvée dont nous avons besoin pour lancer un nouveau service aussi ambitieux.";

			var result = target.CountCharacters(text, "fr-FR");

			Assert.AreEqual(246, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Spanish_Text()
		{
			var target = CreateTarget();

			var text =
				"Nuestros fanáticos de los deportes exigen un acceso inigualable en cualquier momento, en cualquier lugar y en varios dispositivos. Trabajar con el equipo nos brinda la experiencia y la tecnología comprobada que necesitamos para lanzar un nuevo servicio tan ambicioso.";

			var result = target.CountWords(text, "es-ES");

			Assert.AreEqual(40, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Spanish_Text()
		{
			var target = CreateTarget();

			var text =
				"Nuestros fanáticos de los deportes exigen un acceso inigualable en cualquier momento, en cualquier lugar y en varios dispositivos. Trabajar con el equipo nos brinda la experiencia y la tecnología comprobada que necesitamos para lanzar un nuevo servicio tan ambicioso.";

			var result = target.CountCharacters(text, "es-ES");

			Assert.AreEqual(267, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_German_Text()
		{
			var target = CreateTarget();

			var text =
				"Unsere Sportfans fordern einen konkurrenzlosen Zugang zu jeder Zeit, an jedem Ort und auf einer Reihe von Geräten. Durch die Zusammenarbeit mit dem Team verfügen wir über das Fachwissen und die bewährte Technologie, die wir für die Einführung eines so ehrgeizigen neuen Dienstes benötigen.";

			var result = target.CountWords(text, "de-DE");

			Assert.AreEqual(44, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_German_Text()
		{
			var target = CreateTarget();

			var text =
				"Unsere Sportfans fordern einen konkurrenzlosen Zugang zu jeder Zeit, an jedem Ort und auf einer Reihe von Geräten. Durch die Zusammenarbeit mit dem Team verfügen wir über das Fachwissen und die bewährte Technologie, die wir für die Einführung eines so ehrgeizigen neuen Dienstes benötigen.";

			var result = target.CountCharacters(text, "de-DE");

			Assert.AreEqual(289, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Polish_Text()
		{
			var target = CreateTarget();

			var text =
				"Nasi fani sportu wymagają niezrównanego dostępu w dowolnym miejscu, czasie i na wielu urządzeniach. Współpraca z zespołem daje nam wiedzę i sprawdzoną technologię, której potrzebujemy do wprowadzenia tak ambitnej nowej usługi.";

			var result = target.CountWords(text, "pl-PL");

			Assert.AreEqual(31, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Polish_Text()
		{
			var target = CreateTarget();

			var text =
				"Nasi fani sportu wymagają niezrównanego dostępu w dowolnym miejscu, czasie i na wielu urządzeniach. Współpraca z zespołem daje nam wiedzę i sprawdzoną technologię, której potrzebujemy do wprowadzenia tak ambitnej nowej usługi.";

			var result = target.CountCharacters(text, "pl-PL");

			Assert.AreEqual(226, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Words_for_Vietnamese_Text()
		{
			var target = CreateTarget();

			var text =
				"Người hâm mộ thể thao của chúng tôi yêu cầu truy cập vô song mọi lúc, mọi nơi và trên một số thiết bị. Làm việc với nhóm cung cấp cho chúng tôi chuyên môn và công nghệ đã được chứng minh, chúng tôi cần triển khai một dịch vụ mới đầy tham vọng như vậy.";

			var result = target.CountWords(text, "vi-VN");

			Assert.AreEqual(56, result);
		}

		[Test]
		public void Should_be_possible_to_Count_Characters_for_Vietnamese_Text()
		{
			var target = CreateTarget();

			var text =
				"Người hâm mộ thể thao của chúng tôi yêu cầu truy cập vô song mọi lúc, mọi nơi và trên một số thiết bị. Làm việc với nhóm cung cấp cho chúng tôi chuyên môn và công nghệ đã được chứng minh, chúng tôi cần triển khai một dịch vụ mới đầy tham vọng như vậy.";

			var result = target.CountCharacters(text, "vi-VN");

			Assert.AreEqual(251, result);
		}


		private static TextAnalyzer CreateTarget()
		{
			return new TextAnalyzer();
		}
	}
}
