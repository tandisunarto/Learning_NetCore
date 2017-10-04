using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Async
{
    public partial class GeneralizedAsyncReturnTypes : Form
    {
        double timerTTL = 10.0D;
        private DateTime timeTolLive;
        private int cacheValue;

        public GeneralizedAsyncReturnTypes()
        {
            InitializeComponent();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            lblTimer.Text = $"Timer TTL {timerTTL} sec (Stopped)";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timerTTL == 0)
                timerTTL = 5;
            else
                timerTTL -= 1;
            lblTimer.Text = $"Timer TTL {timerTTL} sec (Running)";
        }

        public async Task<int> GetValue()
        {
            await Task.Delay(2000);

            Random r = new Random();
            cacheValue = r.Next();
            timeTolLive = DateTime.Now.AddSeconds(timerTTL);
            timer1.Start();
            return cacheValue;
        }

        public ValueTask<int> LoadReadCache(out bool cached)
        {
            if (timeTolLive < DateTime.Now)
            {
                cached = false;
                return new ValueTask<int>(GetValue());
            }
            else
            {
                cached = true;
                return new ValueTask<int>(cacheValue);
            }
        }


        private async void btnTestAsync_Click(object sender, EventArgs e)
        {
            int value = await LoadReadCache(out bool isCached);

            if (isCached)
            {
                txtOutput.Text = $"Cached value {value} read";
            }
            else
            {
                txtOutput.Text = $"New value {value} read";
            }
        }
    }
}
