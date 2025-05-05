using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;





namespace ENCREYPTION_Project
{
    public partial class Form1 : Form
    {
        char[] alpha = new[] { 'ا', 'ب', 'ت', 'ث', 'ج', 'ح', 'خ', 'د', 'ذ', 'ر', 'ز', 'س', 'ش', 'ص', 'ض', 'ط', 'ظ', 'ع', 'غ', 'ف', 'ق', 'ك', 'ل', 'م', 'ن', 'ه', 'و', 'ي' };
        //static int m = 28; // عدد الحروف في اللغة العربية
        //static int a = 5;  // مفتاح التشفير a
        //static int b = 8;  // مفتاح التشفير b
        //static int c1;
        //int m;
        //int c;
        static string latinAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static char[] letter = latinAlpha.ToCharArray();
        RSAParameters public_key;
        RSAParameters privte_key;
        private string decryptedText;

        public Form1()
        {
            InitializeComponent();
            RSACryptoServiceProvider n = new RSACryptoServiceProvider(2048);
            public_key = n.ExportParameters(false);
            privte_key = n.ExportParameters(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox3.Visible = false;
            groupBox1.Visible = true;
            label26.Text = "Caesar";
            //radioButton2.Visible=false;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            groupBox3.Visible = true;
            groupBox1.Visible = false;
            groupBox5.Visible = false;
        }

         private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox3.Visible = false;
            groupBox1.Visible = true;
            label26.Text = "Playfair";
        }
         private void button12_Click(object sender, EventArgs e)
         {
         //    int p = int.Parse(textBox1.Text);
         //    int g = int.Parse(textBox1.Text);
         //    int prse = int.Parse(textBox1.Text);
         //    int prer = int.Parse(textBox1.Text);

         //    long pubs = ModPow(g, prse, p);
         //    long pubr = ModPow(g, prer, p);

         //    long secretkey = ModPow(pubr, prse, p);

         //    string palnText = textBox1.Text;
         //    string cipherText = CaeserDecrypt(cipherText, (int)secretkey);
         //    textBox1.Text = cipherText;

         //    string decryptedText = CaeserDecrypt(cipherText, (int)secretkey);
         //    textBox1.Text = decryptedText;

         }
        //static string alpha
         private void button5_Click(object sender, EventArgs e)
         {
             groupBox2.Visible = false;
             groupBox5.Visible = false;
             groupBox3.Visible = false;
             groupBox1.Visible = false;
             groupBox4.Visible = true;
         }
         private void button4_Click(object sender, EventArgs e)
         {
             groupBox1.Visible = false;
             groupBox2.Visible = false;
             groupBox4.Visible = false;
             groupBox5.Visible = true;
             groupBox3.Visible = false;
         }
        private void button3_Click_1(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox3.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }


        private void button7_Click(object sender, EventArgs e)
        {
            int index;
            bool numeric = Int32.TryParse(textBox3.Text, out index);
            textBox2.Text = "";
            char[] input = textBox1.Text.ToArray();
            if (radioButton1.Checked == true)
            {
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    if (input[i] == ' ') // تم تعديل '' إلى ' ' (مسافة)
                    {
                        textBox2.Text += " ";
                        continue;
                    }
                    for (int j = 0; j < 28; j++)
                    {
                        if (input[i] == alpha[j])
                        {
                            textBox2.Text += alpha[(j + index) % 28];
                        }
                    }
                }
            }
            else if (radioButton2.Checked == true)
            {
                char[] matrix = (textBox3.Text + new string(alpha)).ToCharArray();
                for (int i = 0; i < matrix.Length; i++)
                {
                    if (matrix[i] == 'j')
                    {
                        matrix[i] = 'i';
                    }
                }
                for (int i = 0; i < matrix.Length; i++)
                    for (int j = i + 1; j < matrix.Length; j++) // تجنب مقارنة نفس العنصر
                    {
                        if (matrix[i] == matrix[j])
                        {
                            matrix = (new string(matrix)).Remove(j, 1).ToCharArray();
                            j--; // إعادة ضبط المؤشر بعد الإزالة
                        }
                    }

                char[,] matrix2D = new char[5, 5];
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        matrix2D[i, j] = matrix[i * 5 + j];
                    }
                }

                char[] words = textBox1.Text.ToCharArray();
                for (int j = 0; j < words.Length; j += 2)
                {
                    char firstletter, secondletter;
                    if (words[j] == 'j') { words[j] = 'i'; }
                    if (j + 1 < words.Length && words[j + 1] == 'j') { words[j + 1] = 'i'; }

                    if (j + 1 == words.Length)
                    {
                        firstletter = words[j];
                        secondletter = 'x';
                    }
                    else if (words[j] == words[j + 1])
                    {
                        firstletter = words[j];
                        secondletter = 'x';
                        j--; // تعديل لتجنب تكرار نفس الحرف
                    }
                    else
                    {
                        firstletter = words[j];
                        secondletter = words[j + 1];
                    }

                    int firstx = 0, firsty = 0, secondx = 0, secondy = 0;
                    for (int row = 0; row < 5; row++)
                    {
                        for (int col = 0; col < 5; col++)
                        {
                            if (firstletter == matrix2D[row, col])
                            {
                                firstx = row;
                                firsty = col;
                            }
                            if (secondletter == matrix2D[row, col])
                            {
                                secondx = row;
                                secondy = col;
                            }
                        }
                    }

                    if (firstx == secondx)
                    {
                        textBox2.Text += matrix2D[firstx, (firsty + 1) % 5];
                        textBox2.Text += matrix2D[secondx, (secondy + 1) % 5];
                    }
                    else if (firsty == secondy)
                    {
                        textBox2.Text += matrix2D[(firstx + 1) % 5, firsty];
                        textBox2.Text += matrix2D[(secondx + 1) % 5, secondy];
                    }
                    else
                    {
                        textBox2.Text += matrix2D[firstx, secondy];
                        textBox2.Text += matrix2D[secondx, firsty];
                    }
                }
                textBox2.Text += " ";
            }
        }

        private void button8_Click(object sender, EventArgs e) // زر فك التشفير
        {
            int index;
            bool numeric = Int32.TryParse(textBox3.Text, out index);
            textBox2.Text = "";
            char[] input = textBox1.Text.ToArray();
            if (radioButton1.Checked == true) // في حالة التشفير القيصري (Caesar cipher)
            {
                for (int i = 0; i < textBox1.TextLength; i++)
                {
                    if (input[i] == ' ') // التعامل مع المسافات
                    {
                        textBox2.Text += " ";
                        continue;
                    }
                    for (int j = 0; j < 28; j++) // الأبجدية العربية (عدد الحروف 28)
                    {
                        if (input[i] == alpha[j])
                        {
                            // فك التشفير يتم بطرح المؤشر (index) بدلاً من إضافته
                            int pos = (j - index) % 28;
                            // لتجنب النتائج السالبة، نقوم بإضافة 28 (عدد الحروف) مرة أخرى
                            if (pos < 0) pos += 28;
                            textBox2.Text += alpha[pos];
                        }
                    }
                }
            }
            else if (radioButton2.Checked == true) // في حالة استخدام Playfair أو غيره
            {
                char[] matrix = (textBox3.Text + new string(alpha)).ToCharArray();
                for (int i = 0; i < matrix.Length; i++)
                {
                    if (matrix[i] == 'j')
                    {
                        matrix[i] = 'i'; // كما هو في التشفير
                    }
                }

                for (int i = 0; i < matrix.Length; i++)
                {
                    for (int j = i + 1; j < matrix.Length; j++) // تجنب العناصر المكررة
                    {
                        if (matrix[i] == matrix[j])
                        {
                            matrix = (new string(matrix)).Remove(j, 1).ToCharArray();
                            j--; // إعادة ضبط المؤشر بعد الإزالة
                        }
                    }
                }

                char[,] matrix2D = new char[5, 5];
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        matrix2D[i, j] = matrix[i * 5 + j];
                    }
                }

                char[] words = textBox1.Text.ToCharArray();
                for (int j = 0; j < words.Length; j += 2)
                {
                    char firstletter, secondletter;
                    if (words[j] == 'j') { words[j] = 'i'; }
                    if (j + 1 < words.Length && words[j + 1] == 'j') { words[j + 1] = 'i'; }

                    if (j + 1 == words.Length)
                    {
                        firstletter = words[j];
                        secondletter = 'x';
                    }
                    else if (words[j] == words[j + 1])
                    {
                        firstletter = words[j];
                        secondletter = 'x';
                        j--;
                    }
                    else
                    {
                        firstletter = words[j];
                        secondletter = words[j + 1];
                    }

                    int firstx = 0, firsty = 0, secondx = 0, secondy = 0;
                    for (int row = 0; row < 5; row++)
                    {
                        for (int col = 0; col < 5; col++)
                        {
                            if (firstletter == matrix2D[row, col])
                            {
                                firstx = row;
                                firsty = col;
                            }
                            if (secondletter == matrix2D[row, col])
                            {
                                secondx = row;
                                secondy = col;
                            }
                        }
                    }

                    // فك التشفير يعتمد على نفس قواعد تشفير Playfair ولكن بالعكس
                    if (firstx == secondx)
                    {
                        textBox2.Text += matrix2D[firstx, (firsty - 1 + 5) % 5];
                        textBox2.Text += matrix2D[secondx, (secondy - 1 + 5) % 5];
                    }
                    else if (firsty == secondy)
                    {
                        textBox2.Text += matrix2D[(firstx - 1 + 5) % 5, firsty];
                        textBox2.Text += matrix2D[(secondx - 1 + 5) % 5, secondy];
                    }
                    else
                    {
                        textBox2.Text += matrix2D[firstx, secondy];
                        textBox2.Text += matrix2D[secondx, firsty];
                    }
                }
                textBox2.Text += " ";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string plainText = textBox4.Text;
            string key = textBox6.Text;
            string result = "";

            plainText = plainText.ToUpper();
            key = key.ToUpper();
            int keyIndex = 0;

            for (int i = 0; i < plainText.Length; i++)
            {
                if (char.IsLetter(plainText[i]))
                {
                    char c = (char)((plainText[i] + key[keyIndex]) % 26 + 'A');
                    result += c;
                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else
                {
                    result += plainText[i];
                }
            }

            textBox5.Text = result;  // عرض النتيجة المشفرة في textBox5
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string cipherText = textBox5.Text;  // النص المشفر من textBox5
            string key = textBox6.Text;         // المفتاح من textBox6
            string result = "";                 // المتغير الذي سيحمل نتيجة فك التشفير

            cipherText = cipherText.ToUpper();  // تحويل النص المشفر إلى أحرف كبيرة
            key = key.ToUpper();                // تحويل المفتاح إلى أحرف كبيرة
            int keyIndex = 0;

            // حلقة فك التشفير
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (char.IsLetter(cipherText[i])) // إذا كان الحرف الحالي هو حرف
                {
                    // فك التشفير باستخدام المفتاح
                    char c = (char)((cipherText[i] - key[keyIndex] + 26) % 26 + 'A');
                    result += c;
                    keyIndex = (keyIndex + 1) % key.Length; // الانتقال للحرف التالي في المفتاح
                }
                else
                {
                    result += cipherText[i]; // إذا كان ليس حرفًا (مثل فراغ أو رمز)، نتركه كما هو
                }
            }

            textBox4.Text = result;  // عرض النتيجة في textBox4 (النص الأصلي بعد فك التشفير)
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string cipherText = textBox8.Text;  // النص المشفر من textBox8
                string key = textBox9.Text;         // المفتاح من textBox9
                string plainText = "";              // المتغير الذي سيحمل النص المفكوك

                // المفتاح يجب أن يحتوي على قيمتين: a و b
                string[] keyParts = key.Split(',');

                // التحقق من أن المفتاح يحتوي على قيمتين على الأقل
                if (keyParts.Length != 2)
                {
                    MessageBox.Show("الرجاء إدخال المفتاح بشكل صحيح (a,b).");
                    return;
                }

                int a = int.Parse(keyParts[0]);
                int b = int.Parse(keyParts[1]);

                // حساب المعكوس الضربي لـ a بالنسبة لـ 26
                int aInverse = ModularInverse(a, 26);

                // فك التشفير
                foreach (char c in cipherText.ToUpper())
                {
                    if (char.IsLetter(c))
                    {
                        // تحويل الحرف إلى قيمته العددية: A = 0, B = 1, ..., Z = 25
                        int y = c - 'A';
                        // تطبيق المعادلة: D(y) = a^(-1) * (y - b) % 26
                        char decryptedChar = (char)(((aInverse * (y - b + 26)) % 26) + 'A');
                        plainText += decryptedChar;
                    }
                    else
                    {
                        plainText += c;  // إذا كان الحرف غير أبجدي، يضاف كما هو
                    }
                }

                textBox7.Text = plainText;  // عرض النص المفكوك في textBox7
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message);
            }
        }

        // دالة لحساب المعكوس الضربي لـ a بالنسبة لـ m باستخدام خوارزمية إقليدس الموسعة
        private int ModularInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                {
                    return x;
                }
            }
            throw new Exception("المعكوس الضربي غير موجود لـ 'a' بالنسبة لـ 26. تأكد أن قيمة 'a' قابلة للعكس.");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider n2 = new RSACryptoServiceProvider();
            n2.ImportParameters(public_key);
            byte[] m = Encoding.Unicode.GetBytes(textBox13.Text);
            byte[] m2 = n2.Encrypt(m, false);
            textBox10.Text = Convert.ToBase64String(m2);
        }
     void button18_Click(object sender, EventArgs e)
      {
          RSACryptoServiceProvider decrpt = new RSACryptoServiceProvider();
          decrpt.ImportParameters(privte_key);
          byte[] c = Convert.FromBase64String(textBox10.Text);
          byte[] c1 = decrpt.Decrypt(c, false);
          textBox14.Text = Encoding.Unicode.GetString(c1);
      }

// دالة لحساب المعكوس الضربي
private long ModularInverse(long a, long m)
{
    a = a % m;
    for (long x = 1; x < m; x++)
    {
        if ((a * x) % m == 1)
        {
            return x;
        }
    }
    throw new Exception("المعكوس الضربي غير موجود.");
}

// دالة لحساب القوة المودولية
private long ModPow(long baseValue, long exponent, long modulus)
{
    long result = 1;
    baseValue = baseValue % modulus;

    while (exponent > 0)
    {
        if ((exponent & 1) == 1) // إذا كان exponent فرديًا
        {
            result = (result * baseValue) % modulus;
        }
        exponent >>= 1; // قسمة exponent على 2
        baseValue = (baseValue * baseValue) % modulus;
    }

    return result;
        }

private void panel1_Paint(object sender, PaintEventArgs e)
{

}

private void button22_Click(object sender, EventArgs e)
{
    textBox15.Clear();
    textBox16.Clear();
    textBox17.Clear();
    textBox18.Clear();
}

private void Form1_Load(object sender, EventArgs e)
{

}

private void label16_Click(object sender, EventArgs e)
{

}

private void button19_Click(object sender, EventArgs e)
{
    textBox10.Clear();
    textBox13.Clear();
    textBox14.Clear();
}

private void groupBox3_Enter(object sender, EventArgs e)
{

}
private void button21_Click(object sender, EventArgs e)
   {
      // char[] letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

       int m = int.Parse(textBox15.Text);
       int b = int.Parse(textBox16.Text);
       textBox18.Text = "";
       string plantext = textBox17.Text;
       int x = 0;
       string ciphertext = "";
       plantext = plantext.ToUpper();
       for (int i = 0; i < plantext.Length; i++)
       {
           for (int j = 0; j < letter.Length; j++)
           {
               if (plantext[i] == letter[j])
               {
                   x = j;
                   ciphertext += letter[(m * x + b) % 26].ToString();
                   textBox18.Text += ciphertext;
               }
           }
       }
  
}

private void button20_Click(object sender, EventArgs e)
{
    textBox17.Text = "";
    int c1 = 0;
    string plantext;
    int m = int.Parse(textBox15.Text);
    int b = int.Parse(textBox16.Text);
    string ciphertext = textBox18.Text;
    int c = 0;
    ciphertext = ciphertext.ToUpper();
    for (int i = 1; i < 26; i++)
    {
        if (((m * i) % 26) == 1)
        {
            c1 = i;
            break;
        }
    }
    for (int i = 0; i < ciphertext.Length; i++)
    {
        for (int j = 0; j < letter.Length; j++)
        {
            if (ciphertext[i] == letter[j])
            {
                c = j;
                int v = (c1 * ((c - b) + 26) % 26);
                plantext = letter[v].ToString();
                textBox17.Text += plantext;
            }
        }
    }
    

}

private void label19_Click(object sender, EventArgs e)
{

}

private void textBox2_TextChanged(object sender, EventArgs e)
{

}

private void groupBox4_Enter(object sender, EventArgs e)
{

}

private void textBox9_TextChanged(object sender, EventArgs e)
{

}

private void label1_Click(object sender, EventArgs e)
{
    groupBox1.Visible = false;
    groupBox2.Visible = false;
    groupBox3.Visible = false;
    groupBox4.Visible = false;
    groupBox5.Visible = false;
}

private void label8_Click(object sender, EventArgs e)
{
    groupBox1.Visible = false;
    groupBox2.Visible = false;
    groupBox3.Visible = false;
    groupBox4.Visible = false;
    groupBox5.Visible = false;
}    
  }

}
