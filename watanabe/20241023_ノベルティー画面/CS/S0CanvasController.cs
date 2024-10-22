using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Text;
using Unity.VisualScripting;

public class S0CanvasController : MonoBehaviour
{
    public GameObject nameObject;
    public GameObject serihuObject;
    public GameObject yajirushiObject;
    public GameObject serihuAreaObject;
    public GameObject charctor_1;
    public GameObject charctor_2;
    public GameObject charctor_3;
    public GameObject charctor_4;
    public GameObject charctor_5;
    public GameObject charctor_6;
    public GameObject charctor_7;
    public GameObject charctor_8;
    public AudioClip sound_1;
    public AudioClip sound_2;
    public AudioClip sound_3;
    public AudioClip sound_4;
    public AudioClip sound_5;
    public AudioClip sound_6;
    public AudioClip sound_7;
    public AudioClip sound_8;
    public AudioClip sound_9;
    public AudioClip sound_10;
    public Sprite bgi_1;
    public Sprite bgi_2;
    public Sprite bgi_3;
    public AudioClip bgm_1;
    public AudioClip bgm_2;
    public AudioClip bgm_3;
    public GameObject bgObject;

    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] TextAsset TextFile;

    [SerializeField]private float captionSpeed = 0.1f;
    private int L_POS = -400;
    private int C_POS = 0;
    private int R_POS = 400;
    private Queue<char> charArray;
    private int textNum = 1;
    private TextMeshProUGUI name_text;
    private TextMeshProUGUI serihu_text;
    private List<string[]> TextData;
    private AudioSource seAoudioSource;
    private AudioSource bgmAoudioSource;
    private Image backGroundImg;

    // Start is called before the first frame update
    void Start()
    {
        // SEオーディオソースを設定
        seAoudioSource = GetComponent<AudioSource>();
        // BGMオーディオソースを設定
        bgmAoudioSource = bgObject.GetComponent<AudioSource>();
        // BackGroundImgを設定
        backGroundImg = bgObject.GetComponent<Image>();

        // 矢印を非表示にする
        yajirushiObject.SetActive(false);

        // セリフテキストを読み込んでリストに格納する
        TextData = readText();
        name_text = nameObject.GetComponent<TextMeshProUGUI> ();
        serihu_text = serihuObject.GetComponent<TextMeshProUGUI> ();
        // 名前を反映する
        name_text.text = TextData[0][0];
        // セリフを1文字ずつ表示させる
        OutReadLine(TextData[0][1]);
        // キャラクターを非表示にする
        charctor_1.SetActive(false);
        charctor_2.SetActive(false);
        charctor_3.SetActive(false);
        charctor_4.SetActive(false);
        charctor_5.SetActive(false);
        charctor_6.SetActive(false);
        charctor_7.SetActive(false);
        charctor_8.SetActive(false);
        // キャラクターを表示させる
        ActiveCharactor(TextData[0][2]);
        ActiveCharactor(TextData[0][3]);
        // SEを再生する
        PlaySound(TextData[0][4]);
        // 背景画像を表示する
        ActiveBackGroundImg(TextData[0][5]);
        // BGMを再生する
        PlayBGM(TextData[0][6]);
    }

    /**
    * セリフテキストファイルを読み込む
    */
    List<string[]> readText()
    {
        List<string[]> TextData = new List<string[]>();

        StringReader reader = new StringReader(TextFile.text);

        while (reader.Peek() != -1) 
        {
            string line = reader.ReadLine();
            TextData.Add(line.Split(','));
        }

        return TextData;
    }

    /**
    * セリフを画面に反映する
    */
    private bool OutputChar()
    {
        // キューに何も格納されていなければfalseを返す
        if (charArray.Count <= 0)
        {
            // 矢印を表示にする
            yajirushiObject.SetActive(true);
            return false;
        } 
        serihu_text.text += charArray.Dequeue();
        return true;
    }

    /**
    * 文字送りするコルーチン
    */
    private IEnumerator ShowChars(float wait)
    {
        // OutputCharメソッドがfalseを返す(=キューが空になる)までループする
        while (OutputChar())
        // wait秒だけ待機
        yield return new WaitForSeconds(wait);
        // コルーチンを抜け出す
        yield break;
    }

    /**
    * 一文字ずつ文字を送る
    */
    private void OutReadLine(string text)
    {
        charArray = SeparateString(text);
        // コルーチンを呼び出す
        StartCoroutine(ShowChars(captionSpeed));
    }

    /**
    * 文を1文字ごとに区切り、キューに格納したものを返す
    */
    private Queue<char> SeparateString(string str)
    {
        // 文字列をchar型の配列にする = 1文字ごとに区切る
        char[] chars = str.ToCharArray();
        Queue<char> charQueue = new Queue<char>();
        // foreach文で配列charsに格納された文字を全て取り出し
        // キューに加える
        foreach (char c in chars) charQueue.Enqueue(c);
        return charQueue;
    }

    /**
    * 対象のキャラ画像を指定の位置に表示する
    */
    private void ActiveCharactor(string charParam)
    {
        string cahrNum;
        string posStr;
        int posNum =0;
        string[] tempList;
        RectTransform  charctor_rt = GetComponent<RectTransform>();
        Vector2 pos = new Vector3(0, 0, 0);
        // キャラクターを表示させない場合はスキップ
        if(charParam == "") return;

        tempList = charParam.Split(':');
        cahrNum = tempList[0];
        posStr = tempList[1];

        // 表示ポジションを設定する
        if (posStr == "c")
        {
            posNum = C_POS;
        }
        else if(posStr == "r")
        {
            posNum = R_POS;
        }
        else if(posStr == "l")
        {
            posNum = L_POS;
        }

        // 表示キャラクターを設定する
        if (cahrNum == "1")
        {
            charctor_rt = charctor_1.GetComponent<RectTransform> ();
            charctor_1.SetActive(true);
        }
        else if(cahrNum == "2")
        {
            charctor_rt = charctor_2.GetComponent<RectTransform> ();
            charctor_2.SetActive(true);
        }
        else if(cahrNum == "3")
        {
            charctor_rt = charctor_3.GetComponent<RectTransform> ();
            charctor_3.SetActive(true);
        }
        else if(cahrNum == "4")
        {
            charctor_rt = charctor_4.GetComponent<RectTransform> ();
            charctor_4.SetActive(true);
        }
        else if(cahrNum == "5")
        {
            charctor_rt = charctor_5.GetComponent<RectTransform> ();
            charctor_4.SetActive(true);
        }
        else if(cahrNum == "6")
        {
            charctor_rt = charctor_6.GetComponent<RectTransform> ();
            charctor_4.SetActive(true);
        }
        else if(cahrNum == "7")
        {
            charctor_rt = charctor_7.GetComponent<RectTransform> ();
            charctor_4.SetActive(true);
        }
        else if(cahrNum == "8")
        {
            charctor_rt = charctor_8.GetComponent<RectTransform> ();
            charctor_4.SetActive(true);
        }

        pos = charctor_rt.anchoredPosition;
        pos.x = posNum;
        charctor_rt.anchoredPosition = pos;
    }

    /**
    * 効果音を再生する
    */
    private void PlaySound(string number)
    {
        if (number == "1")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_1);
        }
        else if(number == "2")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_2);
        }
        else if(number == "3")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_3);
        }
        else if(number == "4")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_4);
        }
        else if(number == "5")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_5);
        }
        else if(number == "6")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_6);
        }
        else if(number == "7")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_7);
        }
        else if(number == "8")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_8);
        }
        else if(number == "9")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_9);
        }
        else if(number == "10")
        {
            seAoudioSource.Stop();
            seAoudioSource.PlayOneShot(sound_10);
        }
    }

    /**
    * BGMを再生する
    */
    private void PlayBGM(string number)
    {
        if (number == "1")
        {
            bgmAoudioSource.Stop();
            bgmAoudioSource.PlayOneShot(bgm_1);
        }
        else if(number == "2")
        {
            bgmAoudioSource.Stop();
            bgmAoudioSource.PlayOneShot(bgm_2);
        }
        else if(number == "3")
        {
            bgmAoudioSource.Stop();
            bgmAoudioSource.PlayOneShot(bgm_3);
        }
        else{

        }
            
        
    }

    /**
    * BGIを設定する
    */
    private void ActiveBackGroundImg(string number)
    {
        if (number == "1")
        {
            backGroundImg.sprite = bgi_1;
        }
        else if(number == "2")
        {
            backGroundImg.sprite = bgi_2;
        }
        else if(number == "3")
        {
            backGroundImg.sprite = bgi_3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject clickObj = null;

        // クリックされたオブジェクトを取得
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
            if (hit2d) 
            {
                clickObj = hit2d.transform.gameObject;
            }   
        }

        // セリフエリアがクリックされた場合に処理を行う
        if ((clickObj != null) && (clickObj.name == serihuAreaObject.name))
        {
            // 矢印が表示されていたら
            if (yajirushiObject.activeSelf)
            {
                // 矢印を非表示にする
                yajirushiObject.SetActive(false);
                // 次のセリフを表示する
                if(TextData.Count > textNum)
                {
                    name_text.text = TextData[textNum][0];
                    serihu_text.text ="";
                    OutReadLine(TextData[textNum][1]);

                    // キャラクターを非表示にする
                    charctor_1.SetActive(false);
                    charctor_2.SetActive(false);
                    charctor_3.SetActive(false);
                    charctor_4.SetActive(false);
                    charctor_5.SetActive(false);
                    charctor_6.SetActive(false);
                    charctor_7.SetActive(false);
                    charctor_8.SetActive(false);
                    // キャラクターを表示させる
                    ActiveCharactor(TextData[textNum][2]);
                    ActiveCharactor(TextData[textNum][3]);

                    // SEを再生する
                    PlaySound(TextData[textNum][4]);
                    // 背景画像を表示する
                    ActiveBackGroundImg(TextData[textNum][5]);
                    // BGMを再生する
                    PlayBGM(TextData[textNum][6]);


                    textNum += 1;
                }
            }
        }
    }

}
