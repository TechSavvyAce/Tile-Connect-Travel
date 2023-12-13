Cau truc mot vai thu muc quan trong
Anh nam trong 2 thu muc
	Resources/Images
	Images
Am thanh
	Sound
Font chu
	Resources/Font

0. Cac cai dat co ban cua game o trong GameConfig.cs(Co giai thich cho nhung bien su dung)
1. Sua id quang cao cua google:
	GameConfig.admob_id_2017
2. Bat tat debug game
	GameConfig.DEBUG_KEY = true;
	Khi bat debug thi an ? se tu dong an
	An vao thanh level ben trai tren cung thi se chuyen level
3. Thanh toan qua google
	Class Purchaser.cs
	
	private static string kProductNameAppleSmallPack =    "com.sunnymonki.pilkachuonline.smallpack";             // Apple App Store identifier for the consumable product.
	private static string kProductNameAppleNormalPack = "com.sunnymonki.pilkachuonline.normalpack";      // Apple App Store identifier for the non-consumable product.
	private static string kProductNameAppleBigPack =  "com.sunnymonki.pilkachuonline.bigpack";       // Apple App Store identifier for the subscription product.

	private static string kProductNameGooglePlaySmallPack =    "com.sunnymonkey.pikachu2017.small";        // Google Play Store identifier for the consumable product.
	private static string kProductNameGooglePlayNormalPack = "com.sunnymonkey.pikachu2017.medium";     // Google Play Store identifier for the non-consumable product.
	private static string kProductNameGooglePlayBigPack =  "com.sunnymonkey.pikachu2017.big";  // Google Play Store identifier for the subscription product.

4. Thanh toan qua Ngan Luong
	Class NganLuong.cs
	Search Object MobileCard va enable len(Hien dang an di vi google khong cho phep them dinh dang thanh toan khac vao)

5. Them level vao game
	-Them file vao thu muc Resources/Level/HardMode1
	-Tang so luong level len trong GameConfig.cs(num_level_hard1,num_level_hard2,num_level)
	{
	"level": 10,
	"time_star_3": 300,  //Thoi gian cac moc de tinh sao(don vi la s)
	"time_star_2": 500,
	"time_star_1": 800,
	"time_dead": 1200,  //Thoi gian gioi han cua man choi
	"auto_gen":[ 	// Che do tao bang sau mot khoang thoi gian
	{
	"type":1,
	"time_gen":6,	//Dong ho quay 6s thi se tao ra bang
	"time_gen_wait":2, // Doi sau 2s tiep tuc tao bang moi
	},
	{
	"type":1,
	"time_gen":7,
	"time_gen_wait":4,
	}
	],
	"constraint" : [
	],
	"reward": [ // Phan thuong sau moi man choi
	{
	"type":0,	// Loai reward 0:random 1:hint 2:Energy(Khong dung)
	"number":2,
	"probability0": 20,
	"probability1": 25,
	"probability2": 40,
	"probability3": 55,
	},
	{
	"type":1,
	"number":2,
	"probability0": 18,
	"probability1": 22,
	"probability2": 40,
	"probability3": 55,
	},
	{
	"type":2,
	"number":1,
	"probability0": 18,
	"probability1": 22,
	"probability2": 40,
	"probability3": 55,
	}
	]
	}