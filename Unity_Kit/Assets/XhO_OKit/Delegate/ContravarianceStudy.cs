using UnityEngine;

namespace XhO_OKit
{
    /// <summary>
    /// in 逆变学习
    /// </summary>
    public class ContravarianceStudy : MonoBehaviour
    {
        class Animal
        {
        }

        class Dog : Animal
        {
        }

        class Program
        {
            delegate Animal Factory<in T>();
            static Animal MakeAnimal()
            {
                return new Animal();
            }

            public static void Main()
            {
                Factory<Animal> animalMaker = MakeAnimal;
                Factory<Dog> dogmaker = animalMaker;//编译器报错，转换失败
                //"无法将类型“Factory<Animal>”隐式转换为“Factory<Dog>"
                //加上in就可以转化了
            }
        }
    }
}

