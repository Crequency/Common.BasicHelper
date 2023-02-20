namespace Common.BasicHelper.Math;

public class Equation
{
    /// <summary>
    /// 解方程 - 二元一次
    /// </summary>
    /// <param name="a1">方程一的系数 a</param>
    /// <param name="b1">方程一的系数 b</param>
    /// <param name="c1">方程一的结果 c</param>
    /// <param name="a2">方程二的系数 a</param>
    /// <param name="b2">方程二的系数 b</param>
    /// <param name="c2">方程二的系数 c</param>
    /// <returns>结果 - x，y - 组成的数组</returns>
    public static double[] SolveEquation(
        double a1, double b1, double c1,
        double a2, double b2, double c2)
    {
        var rst = new double[2];
        rst[1] = (c2 - (a2 * c1 / a1)) / (b2 - (a2 * b1 / a1));
        rst[0] = (c1 - (b1 * rst[1])) / a1;
        return rst;
    }

    /// <summary>
    /// 解方程 - 三元一次
    /// </summary>
    /// <param name="a1">方程一的系数 a</param>
    /// <param name="b1">方程一的系数 b</param>
    /// <param name="c1">方程一的系数 c</param>
    /// <param name="d1">方程一的结果 d</param>
    /// <param name="a2">方程二的系数 a</param>
    /// <param name="b2">方程二的系数 b</param>
    /// <param name="c2">方程二的系数 c</param>
    /// <param name="d2">方程二的结果 d</param>
    /// <returns>结果 - x，y - 组成的数组</returns>
    public static double[] SolveEquation(
        double a1, double b1, double c1, double d1,
        double a2, double b2, double c2, double d2,
        double a3, double b3, double c3, double d3
        )
    {
        var rst = new double[3];
        double na1, nb1, nc1, na2, nb2, nc2, x, y, z;

        na1 = a1 * b2 - a2 * b1; nb1 = a1 * c2 - a2 * c1; nc1 = a1 * d2 - a2 * d1;

        na2 = a1 * b3 - a3 * b1; nb2 = a1 * c3 - a3 * c1; nc2 = a1 * d3 - a3 * d1;

        var y_z = SolveEquation(na1, nb1, nc1, na2, nb2, nc2);
        y = y_z[0]; z = y_z[1];
        x = (d1 - b1 * y - c1 * z) / a1;
        rst[0] = x; rst[1] = y; rst[2] = z;
        return rst;
    }

    /* a1x+b1y+c1z+d1m=e1
     * a2x+b2y+c2z+d2m=e2
     * a3x+b3y+c3z+d3m=e3
     * a4x+b4y+c4z+d4m=e4
     * 
     * a1x=e1-b1y-c1z-d1m
     * x=(e1-b1y-c1z-d1m)/a1
     * 
     * a2(e1-b1y-c1z-d1m)/a1+b2y+c2z+d2m=e2
     * a3(e1-b1y-c1z-d1m)/a1+b3y+c3z+d3m=e3
     * a4(e1-b1y-c1z-d1m)/a1+b4y+c4z+d4m=e4
     * 
     * (a1*b2-a2*b1)y+(a1*c2-a2*c1)z+(a1*d2-a2*d1)m=a1*e2-a2*e1
     * (a1*b3-a3*b1)y+(a1*c3-a3*c1)z+(a1*d3-a3*d1)m=a1*e3-a3*e1
     * (a1*b4-a4*b1)y+(a1*c4-a4*c1)z+(a1*d4-a4*d1)m=a1*e4-a4*e1
     */

    /// <summary>
    /// 解方程组 - 四元一次
    /// </summary>
    /// <param name="a1">方程一的系数 a</param>
    /// <param name="b1">方程一的系数 b</param>
    /// <param name="c1">方程一的系数 c</param>
    /// <param name="d1">方程一的系数 d</param>
    /// <param name="e1">方程一的结果 e</param>
    /// <param name="a2">方程二的系数 a</param>
    /// <param name="b2">方程二的系数 b</param>
    /// <param name="c2">方程二的系数 c</param>
    /// <param name="d2">方程二的系数 d</param>
    /// <param name="e2">方程二的结果 e</param>
    /// <param name="a3">方程三的系数 a</param>
    /// <param name="b3">方程三的系数 b</param>
    /// <param name="c3">方程三的系数 c</param>
    /// <param name="d3">方程三的系数 d</param>
    /// <param name="e3">方程三的结果 e</param>
    /// <param name="a4">方程四的系数 a</param>
    /// <param name="b4">方程四的系数 b</param>
    /// <param name="c4">方程四的系数 c</param>
    /// <param name="d4">方程四的系数 d</param>
    /// <param name="e4">方程四的结果 e</param>
    /// <returns>x_y_z_t 的数组，对应角标分别为 0,1,2,3</returns>
    public static double[] SolveEquation(
        double a1, double b1, double c1, double d1, double e1,
        double a2, double b2, double c2, double d2, double e2,
        double a3, double b3, double c3, double d3, double e3,
        double a4, double b4, double c4, double d4, double e4
        )
    {
        var rst = new double[4];
        double na1, nb1, nc1, nd1, na2, nb2, nc2, nd2, na3, nb3, nc3, nd3, x, y, z, m;
        na1 = a1 * b2 - a2 * b1; nb1 = a1 * c2 - a2 * c1;
        nc1 = a1 * d2 - a2 * d1; nd1 = a1 * e2 - a2 * e1;
        na2 = a1 * b3 - a3 * b1; nb2 = a1 * c3 - a3 * c1;
        nc2 = a1 * d3 - a3 * d1; nd2 = a1 * e3 - a3 * e1;
        na3 = a1 * b4 - a4 * b1; nb3 = a1 * c4 - a4 * c1;
        nc3 = a1 * d4 - a4 * d1; nd3 = a1 * e4 - a4 * e1;
        var y_z_m = SolveEquation(
            na1, nb1, nc1, nd1,
            na2, nb2, nc2, nd2,
            na3, nb3, nc3, nd3);
        y = y_z_m[0]; z = y_z_m[1]; m = y_z_m[2];
        x = (e1 - b1 * y - c1 * z - d1 * m) / a1;
        rst[0] = x; rst[1] = y; rst[2] = z; rst[3] = m;
        return rst;
    }
}
