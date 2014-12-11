#region usings
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using Combinatorics;
using Combinatorics.Collections;
#endregion usings

namespace VVVV.Nodes
{

    public class CombinatoricsBaseNode<T> : IPluginEvaluate
    {
        #region fields & pins
        [Input("Input", Order = 0)]
        protected IDiffSpread<ISpread<T>> FInput;

        [Input("With Repetition", IsToggle = true, DefaultBoolean = false, Order =2 )]
        protected IDiffSpread<bool> FWithRepetition;

        [Output("Output")]
        protected ISpread<ISpread<T>> FOutput;

        [Output("Count")]
        protected ISpread<int> FCount;

        protected List<T> buffer = new List<T>();
        protected GenerateOption go = new GenerateOption();

        #endregion fields & pins

        public virtual void Evaluate(int SpreadMax)
        {
        }
    }

    public class PermutationsNode<T> : CombinatoricsBaseNode<T>
	{
        override public void Evaluate(int SpreadMax)
		{

           if (FInput.IsChanged || FWithRepetition.IsChanged)
           {
              
                int sMax = Math.Max(FInput.SliceCount, FWithRepetition.SliceCount);

                FOutput.SliceCount = 0;
                FCount.SliceCount = 0;

                for (int i = 0; i < sMax; i++)
                {
                    buffer.Clear();

                    FOutput.SliceCount++;

                    if (FWithRepetition[i])
                        go = GenerateOption.WithRepetition;
                    else
                        go = GenerateOption.WithoutRepetition;


                    for (int j = 0; j < FInput[i].SliceCount; j++)
                    {
                        buffer.Add(FInput[i][j]);
                    }

                    Permutations<T> permutations = new Permutations<T>(buffer, go);

                    FCount.Add((int)permutations.Count);

                    FOutput[i].SliceCount = 0;
                    foreach (IList<T> p in permutations)
                    {
                        FOutput[i].AddRange(p);
                    }

                    
                }
                

            }
		}
	}

    #region Permutations Color Node
    //Doesn't work for now:
    //Unable to cast object of type 'VVVV.Utils.VColor.RGBAColor' to type 'System.IComparable`1[VVVV.Utils.VColor.RGBAColor]'

    //[PluginInfo(Name = "Permutations",
    //            Category = "Color",
    //            Help = "Calculates permutations of given input",
    //            Tags = "combinatorics",
    //            Author = "bjo:rn",
    //            Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    //public class PermutationsColor : PermutationsNode<RGBAColor>
    //{
    //}
    #endregion  Permutations Color Node

    #region Permutations Spreads Node
    [PluginInfo(Name = "Permutations", 
                Category = "Spreads", 
                Help = "Calculates permutations of given input", 
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]
    
    public class PermutationsSpreads : PermutationsNode<double>
    {
    }
    #endregion Permutations Spreads Node

    #region Permutations String Node
    [PluginInfo(Name = "Permutations",
                Category = "String",
                Help = "Calculates permutations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]
   
    public class PermutationsString : PermutationsNode<string>
    {
    }
    #endregion Permutations String Node


    public class CombinationsNode<T> : CombinatoricsBaseNode<T>
    {
        #region fields & pins
        [Input("Subset Element Count", DefaultValue = 1, MinValue = 1, Order = 1 )]
        protected IDiffSpread<int> FSubsetElementCount;
        #endregion fields & pins


        override public void Evaluate(int SpreadMax)
        {

            if (FInput.IsChanged || FSubsetElementCount.IsChanged || FWithRepetition.IsChanged)
            {

                int sMax = Math.Max(Math.Max(FInput.SliceCount, FWithRepetition.SliceCount), FSubsetElementCount.SliceCount);

                FOutput.SliceCount = 0;
                FCount.SliceCount = 0;

                for (int i = 0; i < sMax; i++)
                {
                    buffer.Clear();

                    FOutput.SliceCount++;

                    if (FWithRepetition[i])
                        go = GenerateOption.WithRepetition;
                    else
                        go = GenerateOption.WithoutRepetition;


                    for (int j = 0; j < FInput[i].SliceCount; j++)
                    {
                        buffer.Add(FInput[i][j]);
                    }

                    Combinations<T> combinations = new Combinations<T>(buffer, FSubsetElementCount[i], go);

                    FCount.Add((int)combinations.Count);

                    FOutput[i].SliceCount = 0;
                    foreach (IList<T> c in combinations)
                    {
                        FOutput[i].AddRange(c);
                    }

                }

            }
        }
    }

    #region Combinations Color Node
    [PluginInfo(Name = "Combinations",
                Category = "Color",
                Help = "Calculates combinations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class CombinationsColor : CombinationsNode<RGBAColor>
    {
    }
    #endregion Combinations Color Node

    #region Combinations Enum Node
    [PluginInfo(Name = "Combinations",
                Category = "Enumerations",
                Help = "Calculates combinations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class CombinationsEnum : CombinationsNode<EnumEntry>
    {
    }
    #endregion Combinations Enum Node

    #region Combinations Raw Node
    [PluginInfo(Name = "Combinations",
                Category = "Raw",
                Help = "Calculates combinations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class CombinationsRaw : CombinationsNode<System.IO.Stream>
    {
    }
    #endregion Combinations Raw Node

    #region Combinations Spreads Node
    [PluginInfo(Name = "Combinations",
                Category = "Spreads",
                Help = "Calculates combinations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class CombinationsSpreads : CombinationsNode<double>
    {
    }
    #endregion Combinations Spreads Node

    #region Combinations String Node
    [PluginInfo(Name = "Combinations",
                Category = "String",
                Help = "Calculates combinations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class CombinationsString : CombinationsNode<string>
    {
    }
    #endregion Combinations String Node

    #region Combinations Transform Node
    [PluginInfo(Name = "Combinations",
                Category = "Transform",
                Help = "Calculates combinations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class CombinationsTransform : CombinationsNode<Matrix4x4>
    {
    }
    #endregion Combinations Transform Node

    public class VariationsNode<T> : CombinatoricsBaseNode<T>
    {
        #region fields & pins
        [Input("Subset Element Count", IsSingle = true, DefaultValue = 1, MinValue = 1, Order = 1)]
        protected IDiffSpread<int> FSubsetElementCount;
        #endregion fields & pins

        override public void Evaluate(int SpreadMax)
        {

            if (FInput.IsChanged || FSubsetElementCount.IsChanged || FWithRepetition.IsChanged)
            {

                int sMax = Math.Max(Math.Max(FInput.SliceCount, FWithRepetition.SliceCount), FSubsetElementCount.SliceCount);

                FOutput.SliceCount = 0;
                FCount.SliceCount = 0;

                for (int i = 0; i < sMax; i++)
                {
                    buffer.Clear();

                    FOutput.SliceCount++;

                    if (FWithRepetition[i])
                        go = GenerateOption.WithRepetition;
                    else
                        go = GenerateOption.WithoutRepetition;


                    for (int j = 0; j < FInput[i].SliceCount; j++)
                    {
                        buffer.Add(FInput[i][j]);
                    }

                    Variations<T> variations = new Variations<T>(buffer, FSubsetElementCount[i], go);

                    FCount.Add((int)variations.Count);

                    FOutput[i].SliceCount = 0;
                    foreach (IList<T> v in variations)
                    {
                        FOutput[i].AddRange(v);
                    }

                }

            }
        }
    }

    #region Variations Color Node
    [PluginInfo(Name = "Variations",
                Category = "Color",
                Help = "Calculates variations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class VariationsColor : VariationsNode<RGBAColor>
    {
    }
    #endregion Variations Color Node

    #region Variations Enum Node
    [PluginInfo(Name = "Variations",
                Category = "Enumerations",
                Help = "Calculates variations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class VariationsEnum : VariationsNode<EnumEntry>
    {
    }
    #endregion Variations Color Node

    #region Variations Raw Node
    [PluginInfo(Name = "Variations",
                Category = "Raw",
                Help = "Calculates variations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class VariationsRaw : VariationsNode<System.IO.Stream>
    {
    }
    #endregion Variations Raw Node

    #region Variations Spreads Node
    [PluginInfo(Name = "Variations",
                Category = "Spreads",
                Help = "Calculates variations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class VariationsSpreads : VariationsNode<double>
    {
    }
    #endregion Variations Spreads Node

    #region Variations String Node
    [PluginInfo(Name = "Variations",
                Category = "String",
                Help = "Calculates Variations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class VariationsString : VariationsNode<string>
    {
    }
    #endregion Variations String Node

    #region Variations Transform Node
    [PluginInfo(Name = "Variations",
                Category = "Transform",
                Help = "Calculates Variations of given input",
                Tags = "combinatorics",
                Author = "bjo:rn",
                Credits = "http://www.codeproject.com/Members/Adrian-Akison")]

    public class VariationsTransform : VariationsNode<Matrix4x4>
    {
    }
    #endregion Variations Transform Node

}
