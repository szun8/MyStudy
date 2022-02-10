#include <iostream>
#include <vector>
#include <memory>
#include <algorithm>
using namespace std;
using NodeRef = shared_ptr<struct Node>;    // 전방선언, 생명주기 스스로 관리
// tree ♥️ recursion... 환상의 궁합(트리도 분할을 하면 또 (서브)트리)
struct Node {   // 계층적 구조 <-> graph : 동등한 구조
    Node(){}
    Node(const string& data) : data(data) {}
    string          data;
    vector<NodeRef> children;
};

NodeRef CreateTree(){
    NodeRef root = make_shared<Node>("R1 개발실");
    {   // 디자인
        NodeRef node = make_shared<Node>("디자인팀");
        root->children.push_back(node);
        {
            NodeRef leaf = make_shared<Node>("전투");
            node->children.push_back(leaf);
        }
        {
            NodeRef leaf = make_shared<Node>("경제");
            node->children.push_back(leaf);
        }
        {
            NodeRef leaf = make_shared<Node>("스토리");
            node->children.push_back(leaf);
        }
    }
    {   // 프로그래밍
        NodeRef node = make_shared<Node>("프로그래밍팀");
        root->children.push_back(node);
        {
            NodeRef leaf = make_shared<Node>("서버");
            node->children.push_back(leaf);
        }
        {
            NodeRef leaf = make_shared<Node>("클라");
            node->children.push_back(leaf);
        }
        {
            NodeRef leaf = make_shared<Node>("엔진");
            node->children.push_back(leaf);
        }
    }
    {   // 아트
        NodeRef node = make_shared<Node>("아트팀");
        root->children.push_back(node);
        {
            NodeRef leaf = make_shared<Node>("배경");
            node->children.push_back(leaf);
        }
        {
            NodeRef leaf = make_shared<Node>("캐릭터");
            node->children.push_back(leaf);
        }
    }

    return root;    // 최종 뿌리노드 반환
}

void PrintTree(NodeRef root, int depth){    // 좀 더 계층적인 구조를 시각화하기 위해 깊이를 저장
    for (int i = 0; i < depth; i++) cout<<"-";
    cout<<root->data<<endl;
    for (NodeRef& child : root->children) PrintTree(child,depth+1);
}

int GetHeight(NodeRef root){
    int height = 1;
    for (NodeRef& child : root->children) height = max(height, GetHeight(child)+1);
    // if(height < GetHeight(child)+1) height = GetHeight(child)+1;
    // 각 subtree마다 가진 높이가 다를 수 있으므로, 그중에 가장 깊은 노드를 추출해야하는 것이 관건.
    // 따라서 max사용으로 최대 비교
    return height;
}
int main(){
    NodeRef root = CreateTree();
    PrintTree(root, 0);
    int height = GetHeight(root);
    cout<<"Tree Height : "<<height<<endl;
    return 0;
}