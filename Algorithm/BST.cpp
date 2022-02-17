#include <iostream>
#include <vector>
#include <list>
#include <queue>
#include <memory>
#include <algorithm>
using namespace std;

// 1. 이진탐색 Binary Search
// - 고정 데이터가 정렬된 상태로 배열 접근 -> 그러나 유동적 데이터를 위해서는 중간삽입/삭제가 느림 -> 해당 단점을 보완하고자 트리(노드기반) 구성
// -- 따라서 연결리스트(임의접근X)로는 접근 불가
// - O(log N) : 원하는 데이터를 찾는 과정의 시간 복잡도 (후보군을 반씩 줄여주기떄문에)

vector<int> numbers;
void BinarySearch(int N){
    int left = 0, right = numbers.size()-1;
    cout<<"탐색 범위 : "<<left<<" ~ "<<right<<endl;
    while(left <= right){
        int mid = (left + right)/2;
        if(N < numbers[mid]) right = mid-1;
        else if(N > numbers[mid]) left = mid+1;
        else {
            cout<<"FIND : "<<N<<endl;
            break;
        }
    }
}
// 2. 이진탐색트리 Binary Search Tree
// - 데이터의 유동적인 삽입/삭제를 위해 node기반의 tree로 자료를 저장하여 탐색

//      [num]
// [small] [Big] => rule
// * nil (node) : 데이터가 없는 노드(default node)

struct  Node {
    Node*   parent;
    Node*   left;
    Node*   right;
    int     key;
};

class BinarySearchTree{
public:
    void print_inorder(){ print_inorder(root); cout<<endl; }
    void print_inorder(Node* node);
    // 전위순회 preorder 중위순회 inorder 후위순회 postorder

    void insert(int key);

    void Delete(int key);    // 삭제하고자 하는 값을 가진 노드 탐색
    void Delete(Node* node);
    // 경우의수 : 1. 자식 X 2. 자식 1 3. 자식 2
    void replace(Node* u, Node* v);

    Node* search(Node* node, int key);  // recursion
    Node* search2(Node* node, int key); // 재귀보단 호출스택쌓이는 것이 줄기때문에 성능적인 부분에선 이득

    Node* min(Node* node);    
    Node* max(Node* node);    
    Node* next(Node* node);   // 내 다음 큰 수를 반환해줘

private:
    Node* root;
};

void BinarySearchTree::print_inorder(Node* node){
    if(node == nullptr) return;

    cout<<node->key<<" ";
    print_inorder(node->left);
    print_inorder(node->right);
}

void BinarySearchTree::insert(int key){
    Node* newNode = new Node();
    newNode->key = key;

    if(root==nullptr){
        root =  newNode;
        return ;
    }

    Node* node = root;
    Node* parent = nullptr;
    // key가 넣어질 node 탐색
    while(node){    // node가 유효한 곳(nullptr아닌 곳)까지 적합한 곳 탐색
        parent = node;
        if(key < node->key) node = node->left;
        else node = node->right;
    }

    newNode->parent = parent;
    if(key < parent->key) parent->left = newNode;
    else parent->right = newNode;
}
void BinarySearchTree::Delete(int key){
    Node* deleteNode = search(root, key);
    Delete(deleteNode);
}
void BinarySearchTree::Delete(Node* node){
    if(node == nullptr) return;
    if(node->left == nullptr) replace(node, node->right);   // 자식이 없거나 한명인 경우 둘다 가능
    else if(node->right == nullptr) replace(node, node->left);
    else{   // 자식이 두명일때 ->  
        Node* Next = next(node);    // 대체할 다음 데이터를 찾고
        node->key = Next->key;      // 해당 데이터의 key를 삭제할 노드의 값 대체후
        Delete(Next);               // 대체된 다음 데이터를 삭제해주는 과정
    }
}
// u 서브트리를 v 서브트리로 교체
// 그리고 delete u
void BinarySearchTree::replace(Node* u, Node* v){
    if(u->parent==nullptr) root = v;
    else if(u == u->parent->left) u->parent->left = v;
    else u->parent->right = v;

    if(v) v->parent = u->parent;
    delete u;
}

Node* BinarySearchTree::search(Node* node, int key){
    if(node==nullptr || node->key==key) return node;

    if (node->key > key) return search(node->left, key);
    else return search(node->right, key); 
}

Node* BinarySearchTree::search2(Node* node, int key){
    while(node && node->key != key){
        if(node->key > key) node = node->left;
        else node = node->right;
    }
    return node;
}

Node* BinarySearchTree::min(Node* node){
    while(node->left!=nullptr) node=node->left;

    return node;
}   
Node* BinarySearchTree::max(Node* node){
    while(node->right!=nullptr) node=node->right;

    return node;
}
Node* BinarySearchTree::next(Node* node){
    if(node->right) return min(node->right);

    // 만약 node의 right가 없는 경우 -> 위로 올라가서 그 다음 노드를 추적해줘야함.
    // 그런데 내가 부모의 왼쪽노드라면 그 다음 큰 수를 부모가 될 것이지만,
    // 내가 부모의 오른쪽노드라면, 부모보다 내가 더 크기에 조상을 찾아야하는 경우 발생
    Node* parent = node->parent;
    while(parent && node==parent->right){
        node = parent;              // 나는 부모로 갱신
        parent = parent->parent;    // 부모는 부모의부모인 조부모로 갱신
    }
    return parent;
}
int main(){
    BinarySearchTree BST;
    BST.insert(20);
    BST.insert(10);     // root
    BST.insert(30);
    BST.insert(25);
    BST.insert(26);
    BST.insert(40);
    BST.insert(50);

    BST.print_inorder();
    BST.Delete(20);
    BST.print_inorder();
    BST.Delete(26);
    BST.print_inorder();
    //BinarySearch(82);
    return 0;
}