# Puzzle 

## Bidirectional

>We know two things - the initial, scrambled configuration (call it S) and the final, solved one (call it T). Until now, we have been searching from S to T, but the problem is symmetric (could search from T to S). In fact, this is a better idea because T is fixed, and S is different in every instance of the problem.
>A better idea is to run two BFS processes ­ one from S forward and one from T backward. Both will grow BFS trees with configurations as the vertices. If we ever hit the same vertex from both sides, then we are done. This vertex (call it v) is an intersection point between the BFS tree of S and the BFS tree of T, so we can trace the path from S to v and append to it the path from v to T, and we get a shortest path from S to T.

>Bidirectional search works in many situations where we are searching for a path of some sort from X to Y, and each move on the path is reversible to allow for a backwards search. To implement the two searches, we could either alternate between them, making one move at a time (pop from the BFS queue and explore neighbours), or we could run one search to a fixed depth, and then run the other search.

# Resources

[uninformed search](http://cse3521.artifice.cc/uninformed-search.html)


