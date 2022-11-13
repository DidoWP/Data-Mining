# Data-Mining
Excersices and homeworks from Data Mining course from my 4th year at university

1. FrogLeepPuzzle - Informed search
  a) Realisation - DFS
  b) Optimisations - because there are awlays only 2 possible paths to the result, I limit the DFS to the half of the tree and generate the other solution as mirror image
  c) Link to the game - https://data.bangtech.com/algorithm/switch_frogs_to_the_opposite_side.htm
 
   Input: N - number of frogs looking in one direction
   Output: All the configurations that needed to go through to get from the start to the final state.

2. EightPuzzle - Uninformed search
  a) Realisation - IDA* with Manhattan heuristic
  b) Optimisations - increase the threshold with the min Manhattan value among the unvisited children
  c) Link to the game - https://appzaza.com/tile-slide-game
  d) Requirements - should work under 1 second for puzzles that need 40 moves to reach the result state.
  
   Input: N - number of tiles on the board (3x3 board have 8 tiles, 4x4 have 15 tiles, 5x5 have 24, etc.),
          L - position of the 0 (empty tile) at the result board (-1 is default - bottom right),
          NxN matrix - initial board
   Output: Number of moves needed and the moves in words (left, up, right, down)
  
  3.NQueens - local optimisations
    a) Realisation - min-conflict (hill-climbing)
    b) Optimisations - initialise the initial possitions of the queens with min-conflict
    c) Requirements - should work under 1 second for 10000 queens.
    
    Input: N - number of the queens
    Output: 
