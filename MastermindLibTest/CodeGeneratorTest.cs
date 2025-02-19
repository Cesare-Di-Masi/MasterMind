using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MastermindLib;

namespace MastermindLibTest
{
    [TestClass]
    public class CodeGeneratorTest
    {
        [TestMethod]
        public void CodeGenerator_GenerateCode_Complexity_Works()
        {
            CodeGenerator generator = new CodeGenerator(6, 4, 1);

            Colours[] col = generator.generateCode();
            int[] check = new int[col.Length];
            bool cor = false;

            for(int i = 0; i<col.Length; i++)
            {
                check[(int)col[i]]++;
                if(check[(int)col[i]] > 1)
                    cor = true;
            }

            Assert.AreEqual(false, cor);

        }



    }
}
