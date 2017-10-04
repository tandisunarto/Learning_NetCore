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

        public async Task<(int, bool)> GetValue()
        {
            await Task.Delay(2000);

            Random r = new Random();
            cacheValue = r.Next();
            timeTolLive = DateTime.Now.AddSeconds(timerTTL);
            timer1.Start();
            return (cacheValue, false);
        }

        public ValueTask<(int, bool)> LoadReadCache()
        {
            if (timeTolLive < DateTime.Now)
                return new ValueTask<(int, bool)>(GetValue());
            else
                return new ValueTask<(int, bool)>((cacheValue, true));
        }

        private async void btnTestAsync_Click(object sender, EventArgs e)
        {
            (int value, bool isCached) = await LoadReadCache();

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
