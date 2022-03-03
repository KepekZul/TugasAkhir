using System;
using Emgu.CV;

namespace Gabor
{
    public enum GABOR_TYPE
    {GABOR_REAL=0, GABOR_IMAG, GABOR_MAG, GABOR_PHASE}

    public class GaborKernel
    {
        double _sigma = 2 * Math.PI;
        double _f = Math.Sqrt(2.0);
        double _kMax = Math.PI / 2;
        double _kv;
        double _phi;
        bool _bInitialized;
        bool _bKernel;
        int _width;
        int _PIDividen = 8;
        Matrix<float> _Imag;
        Matrix<float> _Real;

        public GaborKernel(int iMu, int iNu)
        {
            _bInitialized = false;
            _bKernel = false;

            // absolute value of kv
            _kv = _kMax / Math.Pow(_f, (double)iNu);
            _phi = Math.PI * iMu / _PIDividen;

            _bInitialized = true;
            _width = GetMaskWidth();

            CreateKernel();
        }

        private void CreateKernel()
        {
            if (!_bInitialized) throw new Exception("Not initialized before creating kernel.");

            _Imag = new Matrix<float>(_width, _width);
            _Real = new Matrix<float>(_width, _width);

            int x, y;
            double dReal;
            double dImag;
            double dTemp1, dTemp2, dTemp3;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    x = i - _width / 2;
                    y = j - _width / 2;
                    dTemp1 = (Math.Pow(_kv, 2) / Math.Pow(_sigma, 2)) *
                        Math.Exp(-(Math.Pow((double)x, 2) + Math.Pow((double)y, 2)) * Math.Pow(_kv, 2) / (2 * Math.Pow(_sigma, 2)));
                    dTemp2 = Math.Cos(_kv * Math.Cos(_phi) * x + _kv * Math.Sin(_phi) * y) - Math.Exp(-(Math.Pow(_sigma, 2) / 2));
                    dTemp3 = Math.Sin(_kv * Math.Cos(_phi) * x + _kv * Math.Sin(_kv) * y);

                    dReal = dTemp1 * dTemp2;
                    dImag = dTemp1 * dTemp3;

                    _Real.Data[i, j] = (float)dReal;
                    _Imag.Data[i, j] = (float)dImag;
                }
            }

            _bKernel = true;
        }

        private int GetMaskWidth()
        {
            if (!_bInitialized) throw new Exception("Not initialized before calling GetMastWidth().");

            double dModSigma = _sigma / _kv;
            int iWidth = (int)Math.Floor(dModSigma * 6 + 1);
            if ((iWidth & 1) == 0) iWidth++;
            return iWidth;
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public Matrix<float> OddKernel
        {
            get { return _Imag; }
        }

        public Matrix<float> EvenKernel
        {
            get
            {
                return _Real;
            }
        }
    }
}
