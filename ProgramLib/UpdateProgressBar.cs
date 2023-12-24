using System.Windows.Forms;

namespace ProgramLib {
    public class UpdateProgressBar {
        ProgressBar progress;
        public UpdateProgressBar(ProgressBar progress) => this.progress = progress;
        public void Initialize(int maximum) {
            if (progress != null) {
                progress.Maximum = maximum;
                progress.Value = 0;
            }
        }
        public void Increment() {
            if(progress != null) progress.Value++;
        }
    }
}
