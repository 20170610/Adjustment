using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SingleLeveling
{
    /// <summary>
    /// ���Է��̣���Bx+V=0
    /// </summary>
    public class Indirect
    {
        public Matrix B
        {
            get;
            set;
        }
        public Matrix l
        {
            get;
            set;
        }
        public Matrix P
        {
            get;
            set;
        }
        
        /// <summary>
        /// x��ά��
        /// </summary>
        public int N
        {
            get
            {
                return B.Rows;
            }
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="B">ϵ������</param>
        /// <param name="l">�۲�ֵ</param>
        /// <param name="P">Ȩ����Ϊnull�����ʼ��һ��E</param>
        public Indirect(Matrix B, Matrix l, Matrix P = null)
        {
            if (B.Rows != l.Rows)
            {
                throw new Exception("B��V�����������");
            }
            else
            {
                this.B = B;
                this.l = l;
                if (P == null)
                {
                    double[,] E = new double[B.Rows, B.Rows];
                    for (int i = 0; i < B.Rows; i++)
                    {
                        for (int j = 0; j < B.Rows; j++)
                        {
                            if (i == j)
                            {
                                E[i, j] = 1;
                            }
                        }
                    }
                    this.P = new Matrix(E);
                }
                else
                {
                    this.P = P;
                }
            }
        }

        #region ��ⷽ��
        public Matrix GetN()
        {
            return this.B.Transpose() * this.P * this.B;
        }
        public Matrix GetW()
        {
            return this.B.Transpose() * this.P * this.l;
        }
        public Matrix Getx()
        {
            return this.GetN().Inverse() * this.GetW();
        }
        public Matrix GetV()
        {
            return this.B * this.Getx() - this.l;
        }
        public double GetSigma()
        {
            return Math.Sqrt((GetV().Transpose() * P * GetV())[0, 0] / (B.Rows - B.Cols));
        }
        public Matrix GetQvv()
        {
            return P.Inverse() - B * GetN().Inverse() * B.Transpose();
        }
        #endregion
       
        
        public override string ToString()
        {
            Matrix V = this.GetV();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.N; i++)
            {
                for (int j = 0; j < B.Cols; j++)
                    sb.Append(B[i, j] + " ");
                for (int j = 0; j < V.Cols; j++)
                {
                    //�Ƿ�Ҫ�ӿո�
                    string flag = "";
                    if (j != V.Cols - 1) flag = "  ";
                    sb.Append(V[i, j] + flag);
                }
                //�ж��Ƿ���
                string lineflag = "";
                if (i != this.N - 1) lineflag = Environment.NewLine;
                sb.Append(lineflag);
            }
            return sb.ToString();
        }
    }
}