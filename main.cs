#define DEBUG
using System;
using std = System.Console;
namespace a1{

  public static class GlobalVar {
    public static readonly int[] GOAL = new int[] {1,2,3,4,5,6,7,8,0};
  }

  public class program{

    public static void Main(string[] args){

#if DEBUG


      var output = new int[]{1,2,3,4,5,6,7,0,8};

#else 

      var user_input = std.ReadLine();
      var output = ParseInput(user_input);

#endif

      var p = new Puzzle<BreadthFirst>(output);
      p.Begin();

    }
    public static int[] ParseInput(string input){
      int[] parsed_values = new int[9];
      var entries = input.Split(default(string[]),StringSplitOptions.RemoveEmptyEntries);
      if(entries.Length < 8) throw new Exception(string.Format("Length was incorrect {0}", entries.Length));


      for(int i = 0; i < 9; i++){
	var entry = entries[i];

	parsed_values[i] = int.Parse(entry);

      }

      return parsed_values;
    }
  }
}
