
namespace Day7
{
    class Worker
    {
        public Step Step
        {
            get => _step;
            set
            {
                _step = value;
                if (value != null)
                {
                    _seconds = value.Duration;
                }
            }
        }

        private int _seconds;
        private Step _step;

        public Worker()
        {
            
        }
        public Worker(Step step)
        {
            Step = step;
            _seconds = step.Duration;
        }

        public void UpdateWorker()
        {
            _seconds--;
        }

        public bool IsFinished()
        {
            return _seconds == 0;
        }

        public bool IsIdle()
        {
            return Step == null;
        }
    }
}
