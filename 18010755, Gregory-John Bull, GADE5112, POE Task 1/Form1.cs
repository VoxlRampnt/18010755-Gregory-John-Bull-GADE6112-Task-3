using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18010755__Gregory_John_Bull__GADE5112__POE_Task_1
{
    

    public partial class FrmMap : Form
    {
        GameEngine engine;
        Timer timer;
        GameState gameState = GameState.PAUSED;
        public FrmMap()
        {
            InitializeComponent();

            engine = new GameEngine();
            lblMap.Text = engine.GetMapDisplay();
            rchTxtBxList.Text = engine.GetUnitInfo();
            lblRound.Text = "Round: " + engine.Round;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += TimerTick;
        }
        private void TimerTick(object sender, EventArgs e)
        {
            engine.GameLoop();
            UpdateUI();
            if (engine.IsGameOver)
            {
                timer.Stop();
                lblMap.Text = engine.WinningFaction + "Won!\n" + lblMap.Text;
                gameState = GameState.ENDED;
                btnStart.Text = "Restart";
            }
        }
        private void UpdateUI()
        {
            lblMap.Text = engine.GetMapDisplay();
            rchTxtBxList.Text = engine.GetUnitInfo();
            lblRound.Text = "Round: " + engine.Round;
        }
        private void btnStart_Click(object sender, EventArgs e) // will control when games starts is paused and restarted, puse button will be used as save button
        {
            if (gameState == GameState.RUNNING)
            {
                timer.Stop();
                gameState = GameState.PAUSED;
                btnStart.Text = "Start";
            }
            else
            {
                if (gameState == GameState.ENDED)
                {
                    engine.Reset();
                }
                timer.Start();
                gameState = GameState.RUNNING;
                btnStart.Text = "Pause";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            engine.SaveGame();
            lblMap.Text = " GAME SAVED\n" + lblMap.Text;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            engine.LoadGame();
            lblMap.Text = "GAME LOADED\n" + lblMap.Text;
        }
    }//
    public enum GameState
    {
        RUNNING, PAUSED, ENDED
    }
}//
