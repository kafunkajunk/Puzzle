using System;
using std = System.Console;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
namespace a1{

  public static class GlobalVar {
    public static readonly int[] GOAL = new int[] {1,2,3,4,5,6,7,8,0};
  }

  public class program{

    public static void Main(string[] args){

      std.WriteLine("Enter path for puzzle file");
      var user_input = std.ReadLine();
      int[] output;

      if(File.Exists(user_input)){

	var lines = ReadFile(user_input);
	if(lines == null) return;

	RunMultiplePuzzles(lines);

      }else{

	std.WriteLine("File does not appear to exist, trying to parse input");

	output = ParseInput(user_input);
	if(output == null)return;

	var algo = AlgorithmFactory(WriteOutputForAlgorithm(), output );      
	if(algo == null)return;
	var p = new Puzzle(algo);

	p.Begin();
	Console.Read();

      }

    }

    ///<summary>
    ///If the user selects to read a file, the program can be ran for each entry in the file
    ///this method handles such a scenario
    ///</summary>
    public static void RunMultiplePuzzles(List<int[]> InitialStates){

      string algo_input = WriteOutputForAlgorithm();
      if(algo_input == null)return;

      foreach(var item in InitialStates){
	var algo = AlgorithmFactory(algo_input,item);
	var p = new Puzzle(algo);
	p.Begin();
      }

    }


    ///<summary>
    ///converts a string representation of a puzzle's initial state into an array
    ///</summary>
    public static int[] ParseInput(string input){
      int[] parsed_values = new int[9];
      var entries = input.Split(default(string[]),StringSplitOptions.RemoveEmptyEntries);
      if(entries.Length < 8){
	std.WriteLine("Length was incorrect {0}", entries.Length);
	return null;
      }

      for(int i = 0; i < 9; i++){

	var entry = entries[i];
	int temp;
	int.TryParse(entry, out temp);
	parsed_values[i] = temp;

      }

      return parsed_values;
    }

    ///<summary>
    ///Creates an algorithm based on user's input, passes null if invalid which is handled
    ///in the calling function by simply returning.
    ///</summary>
    public static Algorithm AlgorithmFactory(string input, int[] arr){

      switch(input){
	case "1":
	  return new BreadthFirst(arr);
	case "2":
	  return new DepthFirstSearch(arr);
	case "3":
	  return new DepthFirstLimited(arr);
	case "4":
	  return new IterativeDepth(arr);
	case "5":
	  return new BiDirectional(arr);
      }
      std.WriteLine("Invalid Choice, input was: {0}", input);
      return null;
    }
    public static string WriteOutputForAlgorithm(){

      //since we have multiple choices for algorithms, we have to check the user input for their choice
      std.WriteLine("Pick a search algorithm:");
      std.WriteLine("1. Breadth first");
      std.WriteLine("2. Depth first");
      std.WriteLine("3. Depth-limited");
      std.WriteLine("4. Iterative-deepening"); 
      std.WriteLine("5. Bi-directional");

      return std.ReadLine();
    }

    ///<summary>
    ///Reads each line of a file and creates a collection of the initial states. 
    ///</summary>
    public static List<int[]> ReadFile(string Path){

      var rv = new List<int[]>();

      string[] lines = System.IO.File.ReadAllLines(Path);
      foreach(var item in lines){
	var output = ParseInput(item);
	if(output == null) return null;

	rv.Add(output);

      }
      return rv;
    }
  }
}

