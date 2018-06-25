using System;
using System.Collections.Generic;

namespace SingleLeveling
{
    [Serializable]
    public class Matrix
    {
        public double[] element;
        private int rows = 0;
        private int cols = 0;
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public int Rows
        {
            get
            {
                return rows;
            }
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public int Cols
        {
            get
            {
                return cols;
            }
        }
        /// <summary>
        /// ��ȡ�����õ�i�е�j�е�Ԫ��ֵ
        /// </summary>
        /// <param name="i">��i��</param>
        /// <param name="j">��j��</param>
        /// <returns>���ص�i�е�j�е�Ԫ��ֵ</returns>
        public double this[int i, int j]
        {
            get
            {
                if (i < Rows && j < Cols)
                {
                    return element[i * cols + j];
                }
                else
                {
                    throw new Exception("����Խ��");
                }
            }
            set
            {
                element[i * cols + j] = value;
            }
        }
        /// <summary>
        /// �ö�ά�����ʼ��Matrix
        /// </summary>
        /// <param name="m">��ά����</param>
        public Matrix(double[][] m)
        {
            this.rows = m.GetLength(0);
            this.cols = m.GetLength(1);
            int count = 0;
            this.element = new double[Rows * Cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    element[count++] = m[i][j];
                }
            }
        }
        public Matrix(double[,] m)
        {
            this.rows = m.GetLength(0);
            this.cols = m.GetLength(1);
            this.element = new double[this.rows * this.cols];
            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    element[count++] = m[i, j];
                }
            }
        }
        public Matrix(List<List<double>> m)
        {
            this.rows = m.Count;
            this.cols = m[0].Count;
            this.element = new double[Rows * Cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    this[i, j] = m[i][j];
                }
            }
        }
        #region ������ѧ����
        public static Matrix MAbs(Matrix a)
        {
            Matrix _thisCopy = a.DeepCopy();
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    _thisCopy[i, j] = Math.Abs(a[i, j]);
                }
            }
            return _thisCopy;
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="a">��һ������,��b�������ͬ�ȴ�С</param>
        /// <param name="b">�ڶ�������</param>
        /// <returns>���ؾ�����Ӻ�Ľ��</returns>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.cols == b.cols && a.rows == b.rows)
            {
                double[,] res = new double[a.rows, a.cols];
                for (int i = 0; i < a.Rows; i++)
                {
                    for (int j = 0; j < a.Cols; j++)
                    {
                        res[i, j] = a[i, j] + b[i, j];
                    }
                }
                return new Matrix(res);
            }
            else
            {
                throw new Exception("�����������в����");
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="a">��һ������,��b�������ͬ�ȴ�С</param>
        /// <param name="b">�ڶ�������</param>
        /// <returns>���ؾ��������Ľ��</returns>
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.cols == b.cols && a.rows == b.rows)
            {
                double[,] res = new double[a.rows, a.cols];
                for (int i = 0; i < a.Rows; i++)
                {
                    for (int j = 0; j < a.Cols; j++)
                    {
                        res[i, j] = a[i, j] - b[i, j];
                    }
                }
                return new Matrix(res);
            }
            else
            {
                throw new Exception("�����������в����");
            }
        }
        /// <summary>
        /// �Ծ���ÿ��Ԫ��ȡ�෴��
        /// </summary>
        /// <param name="a">��ά����</param>
        /// <returns>�õ�������෴��</returns>
        public static Matrix operator -(Matrix a)
        {
            Matrix res = a;
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.cols; j++)
                {
                    res.element[i * a.cols + j] = -res.element[i * a.cols + j];
                }
            }
            return res;
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="a">��һ������</param>
        /// <param name="b">�ڶ�������,����������Ҫ���һ������������</param>
        /// <returns>������˺��һ���µľ���</returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.cols == b.rows)
            {
                double[,] res = new double[a.rows, b.cols];
                for (int i = 0; i < a.rows; i++)
                {
                    for (int j = 0; j < b.cols; j++)
                    {
                        for (int k = 0; k < a.cols; k++)
                        {
                            res[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }
                return new Matrix(res);
            }
            else
            {
                throw new Exception("���������к��в���");
            }
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="a">��һ������</param>
        /// <param name="num">һ��ʵ��</param>
        /// <returns>������˺���µľ���</returns>
        public static Matrix operator *(Matrix a, double num)
        {
            Matrix res = a;
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.cols; j++)
                {
                    res.element[i * a.cols + j] *= num;
                }
            }
            return res;
        }
        /// <summary>
        /// ����ת��
        /// </summary>
        /// <returns>���ص�ǰ����ת�ú���¾���</returns>
        public Matrix Transpose()
        {
            double[,] res = new double[cols, rows];
            {
                for (int i = 0; i < cols; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        res[i, j] = this[j, i];
                    }
                }
            }
            return new Matrix(res);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns>�����������µľ���</returns>
        public Matrix Inverse()
        {
            //���ԭʼ���󲢲��䣬������Ҫ���һ��
            Matrix _thisCopy = this.DeepCopy();
            if (cols == rows && this.Determinant() != 0)
            {
                //��ʼ��һ��ͬ�ȴ�С�ĵ�λ��
                Matrix res = _thisCopy.EMatrix();
                for (int i = 0; i < rows; i++)
                {
                    //�����ҵ���i�еľ���ֵ���������������к͵�i�л���
                    int rowMax = i;
                    double max = Math.Abs(_thisCopy[i, i]);
                    for (int j = i; j < rows; j++)
                    {
                        if (Math.Abs(_thisCopy[j, i]) > max)
                        {
                            rowMax = j;
                            max = Math.Abs(_thisCopy[j, i]);
                        }
                    }
                    //����i�к��ҵ��������һ��rowMax����
                    if (rowMax != i)
                    {
                        _thisCopy.Exchange(i, rowMax);
                        res.Exchange(i, rowMax);

                    }
                    //����i���������б任������һ����0Ԫ�ػ�Ϊ1
                    double r = 1.0 / _thisCopy[i, i];
                    _thisCopy.Exchange(i, -1, r);
                    res.Exchange(i, -1, r);
                    //��Ԫ
                    for (int j = 0; j < rows; j++)
                    {
                        //�����к�����
                        if (j == i)
                            continue;
                        else
                        {
                            r = -_thisCopy[j, i];
                            _thisCopy.Exchange(i, j, r);
                            res.Exchange(i, j, r);
                        }
                    }
                }
                return res;
            }
            else
            {
                throw new Exception("�����Ƿ����޷�����");
            }
        }
        #region ���رȽ������
        public static bool operator <(Matrix a, Matrix b)
        {
            bool issmall = true;
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    if (a[i, j] >= b[i, j]) issmall = false;
                }
            }
            return issmall;
        }
        public static bool operator >(Matrix a, Matrix b)
        {
            bool issmall = true;
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    if (a[i, j] <= b[i, j]) issmall = false;
                }
            }
            return issmall;
        }
        public static bool operator <=(Matrix a, Matrix b)
        {
            bool issmall = true;
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    if (a[i, j] > b[i, j]) issmall = false;
                }
            }
            return issmall;
        }
        public static bool operator >=(Matrix a, Matrix b)
        {
            bool issmall = true;
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    if (a[i, j] < b[i, j]) issmall = false;
                }
            }
            return issmall;
        }
        public static bool operator !=(Matrix a, Matrix b)
        {
            bool issmall = true;
            issmall = ReferenceEquals(a, b);
            if (issmall) return issmall;
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    if (a[i, j] == b[i, j]) issmall = false;
                }
            }
            return issmall;
        }
        public static bool operator ==(Matrix a, Matrix b)
        {
            bool issmall = true;
            issmall = ReferenceEquals(a, b);
            if (issmall) return issmall;
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Cols; j++)
                {
                    if (a[i, j] != b[i, j]) issmall = false;
                }
            }
            return issmall;
        }
        public override bool Equals(object obj)
        {
            Matrix b = obj as Matrix;
            return this == b;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        public double Determinant()
        {
            if (cols == rows)
            {
                Matrix _thisCopy = this.DeepCopy();
                //����ʽÿ�ν����У�����Ҫ����-1
                double res = 1;
                for (int i = 0; i < rows; i++)
                {
                    //�����ҵ���i�еľ���ֵ������
                    int rowMax = i;
                    double max = Math.Abs(_thisCopy[i, i]);
                    for (int j = i; j < rows; j++)
                    {
                        if (Math.Abs(_thisCopy[j, i]) > max)
                        {
                            rowMax = j;
                            max = Math.Abs(_thisCopy[j, i]);
                        }
                    }
                    //����i�к��ҵ��������һ��rowMax����,ͬʱ����λ������ͬ���ȱ任
                    if (rowMax != i)
                    {
                        _thisCopy.Exchange(i, rowMax);
                        res *= -1;
                    }
                    //��Ԫ
                    for (int j = i + 1; j < rows; j++)
                    {
                        double r = -_thisCopy[j, i] / _thisCopy[i, i];
                        _thisCopy.Exchange(i, j, r);
                    }
                }
                //����Խ��߳˻�
                for (int i = 0; i < rows; i++)
                {
                    res *= _thisCopy[i, i];
                }
                return res;
            }
            else
            {
                throw new Exception("��������ʽ");
            }
        }
        #endregion
        #region ���ȱ任
        /// <summary>
        /// ���ȱ任��������r1�͵�r2��
        /// </summary>
        /// <param name="r1">��r1��</param>
        /// <param name="r2">��r2��</param>
        /// <returns>���ؽ������к���µľ���</returns>
        public Matrix Exchange(int r1, int r2)
        {
            if (Math.Min(r2, r1) >= 0 && Math.Max(r1, r2) < rows)
            {
                for (int j = 0; j < cols; j++)
                {
                    double temp = this[r1, j];
                    this[r1, j] = this[r2, j];
                    this[r2, j] = temp;
                }
                return this;
            }
            else
            {
                throw new Exception("��������");
            }
        }
        /// <summary>
        /// ���ȱ任����r1�г���ĳ�����ӵ�r2��
        /// </summary>
        /// <param name="r1">��r1�г���num</param>
        /// <param name="r2">�ӵ���r2�У�����r2��Ϊ������ֱ�ӽ�r1����num������</param>
        /// <param name="num">ĳ�зŴ�ı���</param>
        /// <returns></returns>
        public Matrix Exchange(int r1, int r2, double num)
        {
            if (Math.Min(r2, r1) >= 0 && Math.Max(r1, r2) < rows)
            {
                for (int j = 0; j < cols; j++)
                {
                    this[r2, j] += this[r1, j] * num;
                }
                return this;
            }
            else if (r2 < 0)
            {
                for (int j = 0; j < cols; j++)
                {
                    this[r1, j] *= num;
                }
                return this;
            }
            else
            {
                throw new Exception("��������");
            }
        }
        /// <summary>
        /// �õ�һ��ͬ�ȴ�С�ĵ�λ����
        /// </summary>
        /// <returns>����һ��ͬ�ȴ�С�ĵ�λ����</returns>
        public Matrix EMatrix()
        {
            if (rows == cols)
            {
                double[,] res = new double[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (i == j)
                            res[i, j] = 1;
                        else
                            res[i, j] = 0;
                    }
                }
                return new Matrix(res);
            }
            else
                throw new Exception("���Ƿ����޷��õ���λ����");
        }
        #endregion
        /// <summary>
        /// �����������ֵ������һ���µĶ���
        /// </summary>
        /// <returns>�����������¶���</returns>
        public Matrix DeepCopy()
        {
            double[,] ele = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    ele[i, j] = this[i, j];
                }
            }
            return new Matrix(ele);
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    str += this[i, j].ToString();
                    if (j != Cols - 1)
                        str += " ";
                    else if (i != Rows - 1)
                        str += Environment.NewLine;
                }
            }
            return str;
        }
    }
}