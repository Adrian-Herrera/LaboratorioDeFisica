using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RequisitosFormula
{
    public static RequisitoStruct MRU_x = new(BaseVariable.Distancia, new TipoVariable[] { BaseVariable.Velocidad, BaseVariable.Tiempo }, Formulary2.Formula_mru_x);
    public static RequisitoStruct MRU_v = new(BaseVariable.Velocidad, new TipoVariable[] { BaseVariable.Distancia, BaseVariable.Tiempo }, Formulary2.Formula_mru_v);
    public static RequisitoStruct MRU_t = new(BaseVariable.Tiempo, new TipoVariable[] { BaseVariable.Distancia, BaseVariable.Velocidad }, Formulary2.Formula_mru_t);
    public static RequisitoStruct Formula_1 = new(BaseVariable.VelocidadFinal,
     new TipoVariable[] { BaseVariable.VelocidadInicial, BaseVariable.Aceleracion, BaseVariable.Tiempo }, Formulary2.Formula_1);
    public static RequisitoStruct Formula_1_vo = new(BaseVariable.VelocidadInicial,
    new TipoVariable[] { BaseVariable.VelocidadFinal, BaseVariable.Aceleracion, BaseVariable.Tiempo }, Formulary2.Formula_1_vo);
    public static RequisitoStruct Formula_1_a = new(BaseVariable.Aceleracion,
    new TipoVariable[] { BaseVariable.VelocidadFinal, BaseVariable.VelocidadInicial, BaseVariable.Tiempo }, Formulary2.Formula_1_a);
    public static RequisitoStruct Formula_1_t = new(BaseVariable.Tiempo,
    new TipoVariable[] { BaseVariable.VelocidadFinal, BaseVariable.Aceleracion, BaseVariable.VelocidadInicial }, Formulary2.Formula_1_t);

    public static RequisitoStruct Formula_2 = new(BaseVariable.Distancia,
    new TipoVariable[] { BaseVariable.VelocidadFinal, BaseVariable.Tiempo, BaseVariable.VelocidadInicial }, Formulary2.Formula_2);
    public static RequisitoStruct Formula_2_vf = new(BaseVariable.VelocidadFinal,
    new TipoVariable[] { BaseVariable.Distancia, BaseVariable.Tiempo, BaseVariable.VelocidadInicial }, Formulary2.Formula_2_vf);
    public static RequisitoStruct Formula_2_vo = new(BaseVariable.VelocidadInicial,
    new TipoVariable[] { BaseVariable.VelocidadFinal, BaseVariable.Distancia, BaseVariable.Tiempo }, Formulary2.Formula_2_vo);
    public static RequisitoStruct Formula_2_t = new(BaseVariable.Tiempo,
    new TipoVariable[] { BaseVariable.VelocidadFinal, BaseVariable.Distancia, BaseVariable.VelocidadInicial }, Formulary2.Formula_2_t);

    public static RequisitoStruct Formula_3 = new(BaseVariable.VelocidadFinal,
    new TipoVariable[] { BaseVariable.VelocidadInicial, BaseVariable.Aceleracion, BaseVariable.Distancia }, Formulary2.Formula_3);
    public static RequisitoStruct Formula_3_vo = new(BaseVariable.VelocidadInicial,
    new TipoVariable[] { BaseVariable.Distancia, BaseVariable.Aceleracion, BaseVariable.VelocidadFinal }, Formulary2.Formula_3_vo);
    public static RequisitoStruct Formula_3_x = new(BaseVariable.Distancia,
    new TipoVariable[] { BaseVariable.VelocidadInicial, BaseVariable.VelocidadFinal, BaseVariable.Aceleracion }, Formulary2.Formula_3_x);
    public static RequisitoStruct Formula_3_a = new(BaseVariable.Aceleracion,
    new TipoVariable[] { BaseVariable.VelocidadInicial, BaseVariable.VelocidadFinal, BaseVariable.Distancia }, Formulary2.Formula_3_a);

    public static RequisitoStruct Formula_4 = new(BaseVariable.Distancia,
    new TipoVariable[] { BaseVariable.VelocidadInicial, BaseVariable.Tiempo, BaseVariable.Aceleracion }, Formulary2.Formula_4);
    public static RequisitoStruct Formula_4_vo = new(BaseVariable.VelocidadInicial,
    new TipoVariable[] { BaseVariable.Distancia, BaseVariable.Tiempo, BaseVariable.Aceleracion }, Formulary2.Formula_4_vo);
    public static RequisitoStruct Formula_4_a = new(BaseVariable.Aceleracion,
    new TipoVariable[] { BaseVariable.VelocidadInicial, BaseVariable.Distancia, BaseVariable.Tiempo }, Formulary2.Formula_4_a);
    public static RequisitoStruct Formula_4_t = new(BaseVariable.Tiempo,
    new TipoVariable[] { BaseVariable.VelocidadInicial, BaseVariable.Distancia, BaseVariable.Aceleracion }, Formulary2.Formula_4_t);

    public static RequisitoStruct Formula_5 = new(BaseVariable.Distancia,
    new TipoVariable[] { BaseVariable.VelocidadFinal, BaseVariable.Tiempo, BaseVariable.Aceleracion }, Formulary2.Formula_5);
    public static RequisitoStruct Formula_5_vf = new(BaseVariable.VelocidadFinal,
    new TipoVariable[] { BaseVariable.Distancia, BaseVariable.Tiempo, BaseVariable.Aceleracion }, Formulary2.Formula_5_vf);
    public static RequisitoStruct Formula_5_a = new(BaseVariable.Aceleracion,
    new TipoVariable[] { BaseVariable.VelocidadFinal, BaseVariable.Distancia, BaseVariable.Tiempo }, Formulary2.Formula_5_a);
    public static RequisitoStruct Formula_5_t = new(BaseVariable.Tiempo,
    new TipoVariable[] { BaseVariable.VelocidadFinal, BaseVariable.Distancia, BaseVariable.Aceleracion }, Formulary2.Formula_5_t);
    public static RequisitoStruct[] Requisitos = {
        MRU_x, MRU_t, MRU_v,
        Formula_1,Formula_1_vo,Formula_1_a,Formula_1_t,
        Formula_2,Formula_2_vf,Formula_2_vo,Formula_2_t,
        Formula_3,Formula_3_vo,Formula_3_x,Formula_3_a,
        Formula_4,Formula_4_vo,Formula_4_a,Formula_4_t,
        Formula_5,Formula_5_vf,Formula_5_a,Formula_5_t
    };
    public static List<RequisitoStruct> FindRequisitos(int variableId)
    {
        List<RequisitoStruct> requisitos = new();
        foreach (RequisitoStruct item in Requisitos)
        {
            if (item.Principal.Id == variableId)
            {
                requisitos.Add(item);
            }
        }
        return requisitos;
    }

}
public struct RequisitoStruct
{
    public TipoVariable Principal;
    public TipoVariable[] Requisitos;
    public delegate float MyDelegate(List<VariableInput> variables);
    public MyDelegate myDelegate;
    delegate float formula();
    public RequisitoStruct(TipoVariable principal, TipoVariable[] requisitos, MyDelegate formula)
    {
        Principal = principal;
        Requisitos = requisitos;
        myDelegate = formula;
    }
}
