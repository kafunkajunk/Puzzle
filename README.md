# Puzzle 

## Bidirectional

>We know two things - the initial, scrambled configuration (call it S) and the final, solved one (call it T). Until now, we have been searching from S to T, but the problem is symmetric (could search from T to S). In fact, this is a better idea because T is fixed, and S is different in every instance of the problem.
>A better idea is to run two BFS processes ­ one from S forward and one from T backward. Both will grow BFS trees with configurations as the vertices. If we ever hit the same vertex from both sides, then we are done. This vertex (call it v) is an intersection point between the BFS tree of S and the BFS tree of T, so we can trace the path from S to v and append to it the path from v to T, and we get a shortest path from S to T.

>Bidirectional search works in many situations where we are searching for a path of some sort from X to Y, and each move on the path is reversible to allow for a backwards search. To implement the two searches, we could either alternate between them, making one move at a time (pop from the BFS queue and explore neighbours), or we could run one search to a fixed depth, and then run the other search.


## Heuristics

Some heuristics are better than others, and the better (more informed) the heuristic is, the fewer nodes it needs to examine in the search tree to find a solution.

To be useful, our heuristic must never overestimate the cost of changing from a given state to the goal state. Such a heuristic is defined as being **admissible**.
A monotonic heuristic is also admissible, assuming there is only one goal state.

>Imagine a maze-navigating robot in a maze that has some windows in the walls. The robot can sometimes see the goal through windows. A hill-climbing or best-first search robot may believe it is close to the goal because it can see it through some windows, but in actuality it is quite far from the goal, and must actually back up to make progress. Instead, however, the robot keeps going deeper and deeper into the maze, enticed by the illusory nearness of the goal.
>If the robot simply kept track of how "deep" it had gone into the maze, and if it had reason to believe that the goal cannot possibly be "this deep," it would back up before going even deeper.



### 8 puzzle specific heuristics

 - (OP): The number of tokens that are out of place.
 - (MD): The sum of "Manhattan-distances" between each token and its goal position.
 - (RC): Number of tokens out of row plus number of tokens out of column.

> The standard heuristic for a square grid is the Manhattan distance. Look at your cost function and find the minimum cost D for moving from one space to an adjacent space. In the simple case, you can set D to be 1. The heuristic on a square grid where you can move in 4 directions should be D times the Manhattan distance:

``` 
    function heuristic(node){
      dx = abs(node.x - goal.x)
      dy = abs(node.y - goal.y)
      return D * (dx + dy)
    }
```

# Resources

[uninformed search](http://cse3521.artifice.cc/uninformed-search.html)


