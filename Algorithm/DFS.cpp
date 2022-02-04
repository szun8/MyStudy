#include <iostream>
#include <vector>
using namespace std;

// DFS : Depth First Search - recursion
struct Vertex {
    int data;
};
vector<Vertex> vertices;    
vector<vector<int> > adjacent;
vector<bool> visited;           // 방문한 목록 저장 -> 아니라면 갔던 곳을 계속 가서 뺑뻉이 돌거임 

void CreateGraph() {
    vertices.resize(6);
    adjacent = vector<vector<int> >(6);

    // 인접 리스트 version
    // adjacent[0].push_back(1);
    // adjacent[0].push_back(3);
    // adjacent[1].push_back(0);
    // adjacent[1].push_back(2);
    // adjacent[1].push_back(3);
    // adjacent[3].push_back(4);
    // adjacent[5].push_back(4);

    // 인접 행렬 version
    vector<int> thing1 ;
    thing1.push_back(0); thing1.push_back(1); thing1.push_back(0); thing1.push_back(1); thing1.push_back(0); thing1.push_back(0);
    adjacent.push_back(thing1);
    
    vector<int> thing2 ;
    thing2.push_back(1); thing2.push_back(0); thing2.push_back(1); thing2.push_back(1); thing2.push_back(0); thing2.push_back(0);
    adjacent.push_back(thing2);

    vector<int> thing3 ;
    thing3.push_back(0); thing3.push_back(0); thing3.push_back(0); thing3.push_back(0); thing3.push_back(0); thing3.push_back(0);
    adjacent.push_back(thing3);

    vector<int> thing4 ;
    thing4.push_back(0); thing4.push_back(0); thing4.push_back(0); thing4.push_back(0); thing4.push_back(1); thing4.push_back(0);
    adjacent.push_back(thing4);

    vector<int> thing5 ;
    thing5.push_back(0); thing5.push_back(0); thing5.push_back(0); thing5.push_back(0); thing5.push_back(0); thing5.push_back(0);
    adjacent.push_back(thing5);

    vector<int> thing6 ;
    thing6.push_back(0); thing6.push_back(0); thing6.push_back(0); thing6.push_back(0); thing6.push_back(1); thing6.push_back(0);
    adjacent.push_back(thing6);
}

// dfs(0)
// - dfs(1)
// -- dfs(2)
// -- dfs(3)
// --- dfs(4)

void DFS(int here) {
    // 모든 인접 정점을 순회한다
    visited[here]=true; // 방문

    // - 인접리스트 version 
    for (int i = 0; i < adjacent[here].size(); i++) {
        int there = adjacent[here][i];
        if(visited[there]==false) DFS(there);
    }
    // - 인접행렬 version 
    for (int there = 0; there < adjacent.size(); there++) {
        if(adjacent[here][there] && visited[there]==false) DFS(there);
    }
}

void DFSAll(){  // 끊어져 있는 정점까지 모두 방문을 하고자 할 때는 모든 정점을 확인
    for (int i = 0; i < adjacent.size(); i++){
        if(visited[i] == false) DFS(i); // 해당 정점이 미방문이라면 방문
    }
}
int main(){
    CreateGraph();
    visited.resize(6,false);
    // DFS(0); 모든 정점을 순회하기 위해서는
    DFSAll();
    return 0;
}