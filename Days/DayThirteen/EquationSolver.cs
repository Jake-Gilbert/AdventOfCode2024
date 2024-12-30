namespace DayThirteen
{
    public class EquationSolver
    {
        private long _ax;
        private long _ay;
        private long _bx;
        private long _by;
        private long _px;
        private long _py;
        public EquationSolver(long ax, long ay, long bx, long by, long px, long py)
        {
            _ax = ax;
            _ay = ay;
            _bx = bx;
            _by = by;
            _px = px;
            _py = py;
        }

        public bool CalculateFewestPresses(out long aPresses, out long bPresses)
        {
            aPresses = ((_px * _by) - (_py * _bx)) / ((_ax * _by) - (_ay * _bx));
            bPresses = ((_ax * _py) - (_ay * _px)) / ((_ax * _by) - (_ay * _bx));
            var a = (_ax * aPresses, _ay * aPresses);
            var b = (_bx * bPresses, _by * bPresses);
            var c = (x: a.Item1 + b.Item1, y: a.Item2 + b.Item2);
            return c.x == _px && c.y == _py;
        }
    }
}
