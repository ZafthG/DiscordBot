//
//                  Database/FoodComponents.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
namespace Physics.Database.Enum
{
    /// <summary>
    /// Lista dos tipos de mensagens empregados para o Log.
    /// </summary>
    internal enum FoodComponents : int
    {
        //  Tipo de prato
        MainCourse,             // 0
        Garnish,                // 1
        Salad,                  // 2
        Dessert,                // 3

        //  Vegano
        Vegan,                  // 4

        //  Componentes da comida.
        HasMilk,                // 5
        HaveAllergicProducts,   // 6
        HaveGluten,             // 7
        AnimalOrigin,           // 8
        HaveEggs,               // 9
        HaveHoney,              // 10
        HavePepper              // 11
    }
}
//
//      Fim do código