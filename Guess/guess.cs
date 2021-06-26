using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


class hw: Form {

	Label lblScore;
	Label lblHighScore;
	Label lblGuess;
	TextBox txtGuess;
	Button btnOK;
	Random random;

	int myNumber;
	int trials;
	int score=0;
	int highscore=0;

	const String HIGHSCORE_FILE="highscore.txt";

	hw() {
		InitComponents();
		InitGame();
	}

	void LoadHighScore() {
		highscore=0;
		if(File.Exists(HIGHSCORE_FILE)) {
			StreamReader sr=File.OpenText(HIGHSCORE_FILE);
			highscore=Int32.Parse(sr.ReadLine());
			sr.Close();
		}
		lblHighScore.Text=String.Format("HighScore: {0}",highscore);
	}

	void SaveHighScore() {
		StreamWriter sw=new StreamWriter(HIGHSCORE_FILE);
		sw.WriteLine(highscore.ToString());
		sw.Close();
		lblHighScore.Text=String.Format("HighScore: {0}",highscore);
	}

	void InitGame() {
		LoadHighScore();
		trials=0;
		myNumber=random.Next(100)+1;
	}

	void InitComponents() {
		random=new Random();

		Text="Guess The Number Game";
		ClientSize=new Size(256,128);
		CenterToScreen();
		FormBorderStyle=FormBorderStyle.FixedSingle;

		lblScore=new Label();
		lblScore.Text=String.Format("Score: {0}",score);
		lblScore.Location=new Point(16,16);
		lblScore.AutoSize=true;

		lblHighScore=new Label();
		lblHighScore.Text=String.Format("HighScore: {0}",highscore);
		lblHighScore.Location=new Point(128,16);
		lblHighScore.AutoSize=true;

		lblGuess=new Label();
		lblGuess.Text="Guess my number from 1 to 100";
		lblGuess.Location=new Point(16,48);
		lblGuess.AutoSize=true;

		txtGuess=new TextBox();
		txtGuess.Location=new Point(16,80);
		txtGuess.Width=128;

		btnOK=new Button();
		btnOK.Text="OK";
		btnOK.Location=new Point(152,80);

		btnOK.Click += new EventHandler(btnOK_OnClicked);

		Controls.Add(lblScore);
		Controls.Add(lblHighScore);
		Controls.Add(lblGuess);
		Controls.Add(txtGuess);
		Controls.Add(btnOK);
	}

	void btnOK_OnClicked(object sender, EventArgs e) {
		int guess=0;
		if(Int32.TryParse(txtGuess.Text,out guess)) {
			trials++;
			if(guess==myNumber) {
				MessageBox.Show(String.Format("Correct! You've guessed it in {0} trial(s)", trials));
				MessageBox.Show("Press OK for the next number");
				score++;

				if(score>highscore) {
					highscore=score;
					SaveHighScore();
				}


				lblScore.Text=String.Format("Score: {0}",score);
				InitGame();
			} else if(trials>=10) {
				MessageBox.Show(String.Format("Wrong! The number is {0}",myNumber));
				MessageBox.Show("Game Over");
				Application.Exit();
			} else if(guess<myNumber) MessageBox.Show("Higher!");
			else if(guess>myNumber) MessageBox.Show("Lower!");
		} else {
			MessageBox.Show("Invalid number!");
		}
		txtGuess.Text="";
	}

	[STAThread]
	static void Main() {
		Application.Run(new hw());
	}
}
