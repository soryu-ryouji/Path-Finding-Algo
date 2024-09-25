# Path Finding Algorithm

## Dijkstra

## Best First Search

## A-Star

1968年发明的A star算法就是把启发式方法（heuristic approaches）如BFS，和常规方法如Dijsktra算法结合在一起的算法。有点不同的是，类似BFS的启发式方法经常给出一个近似解而不是保证最佳解。然而，尽管A star基于无法保证最佳解的启发式方法，A star却能保证找到一条最短路径。

公式表示为：

$$
f(n)=g(n)+h(n)
$$

- $g(n)$ 是在状态空间中从 初始节点 到 n节点 的实际代价
- $h(n)$ 是从n到目标节点最佳路径的估计代价。
- $f(n)$ 是节点n从初始点到目标点的估价函数

A-Star 的基本原理就是不停地找自己周围的点，选出一个新的点作为起点再循环的找。

每次从新的点找周围的点时，如果周围的点已经再开启列表或者关闭列表中，就不进行任何操作。

```text
OPEN = priority queue containing START
CLOSED = empty set
while lowest rank in OPEN is not the GOAL:
    current = remove lowest rank item from OPEN
    add current to CLOSED
    for neighbors of current:
        cost = g(current) + movementcost(current, neighbor)
        if neighbor in OPEN and cost less than g(neighbor):
            remove neighbor from OPEN, because new path is better
        if neighbor in CLOSED and cost less than g(neighbor): ⁽²⁾
            remove neighbor from CLOSED
        if neighbor not in OPEN and neighbor not in CLOSED:
            set g(neighbor) to cost
            add neighbor to OPEN
            set priority queue rank to g(neighbor) + h(neighbor)
            set neighbor's parent to current

reconstruct reverse path from goal to start
by following parent pointers
```

## Reference

1. [bilibili: Unity中实现A星寻路算法](https://www.bilibili.com/video/BV147411u7r5/)
2. [Cheng Wei's Blog: A Star Algorithm 总结与实现](https://scm_mos.gitlab.io/motion-planner/a-star/)