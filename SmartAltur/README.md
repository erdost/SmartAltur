I start off finding all the possible ways of dividing N passengers into the cars. It turns out it is the equivalent of all the partitions of the set of N elements.
(https://en.wikipedia.org/wiki/Partition_of_a_set)

The number of the possible partitions of a set is its Bell number.(https://en.wikipedia.org/wiki/Bell_number) And it goes up very fast.

1, 2, 5, 15, 52, 203, 877, 4140, 21147, 115975, 678570, 4213597, 27644437, 190899322, 1382958545, 10480142147, 82864869804, 682076806159, 5832742205057, ...

For the scope of this assignment, 10 passengers waiting for a ride home, I think the number of calculations, 115975, is acceptable if we use some of the parallelism features of C#. 

There 52 ways of distributing 5 passengers (A,B,C,D,E) into cars.
It starts with putting all of them in one car and ends up with each getting his/her own car.

1) ABCDE
2) ABCD + E
3) ABCE + D
4) ABC + DE
5) ABC + D + E
6) ABDE + C
7) ABD + CE
8) ABD + C + E
9) ABE + CD
10) AB + CDE
11) AB + CD + E
12) ABE + C + D
13) AB + CE + D
14) AB + C + DE
15) AB + C + D + E
16) ACDE + B
17) ACD + BE
18) ACD + B + E
19) ACE + BD
20) AC + BDE
21) AC + BD + E
22) ACE + B + D
23) AC + BE + D
24) AC + B + DE
25) AC + B + D + E
26) ADE + BC
27) AD + BCE
28) AD + BC + E
29) AE + BCD
30) A + BCDE
31) A + BCD + E
32) AE + BC + D
33) A + BCE + D
34) A + BC + DE
35) A + BC + D + E
36) ADE + B + C
37) AD + BE + C
38) AD + B + CE
39) AD + B + C + E
40) AE + BD + C
41) A + BDE + C
42) A + BD + CE
43) A + BD + C + E
44) AE + B + CD
45) A + BE + CD
46) A + B + CDE
47) A + B + CD + E
48) AE + B + C + D
49) A + BE + C + D
50) A + B + CE + D
51) A + B + C + DE
52) A + B + C + D + E

To speed things up I would first calculate the shortest paths for all the N passenger combinations of 1, 2, 3, 4 ... n-1, n.
Again for 5 passengers all the car combinations are

5 C 1: A, B, C, D, E

5 C 2: AB, AC, AD, AE, BC, BD, BE, CD, CE, DE

5 C 3: ABC, ABD, ABE, ACD, ACE, ADE, BCD, BCE, BDE, CDE

5 C 4: ABCD, ABCE, ABDE, ACDE, BCDE

5 C 5: ABCDE

In order to find the shortest route of a car, for example the one with the passengers ABC, I would run 3 times the Dijkstra's single-source shortest paths algorithm with each passenger's destination as the end point.
The smallest result of the 3 calculations would be considered the distance of the path by that car.

I would store them in a hash table with calculated distances as values and their keys would be calculated in a specific way to ensure that ABC would be equal to ACB just in case.

Then I would simply add them according to the order of above 52 cases, the winner would be the one with the smallest sum of the above 52 cases.

