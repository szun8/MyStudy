#include <iostream>
#include <vector>
#include <list>
#include <queue>
#include <memory>
#include <algorithm>
using namespace std;

template<typename T, typename Container = vector<T>, typename Predicate = less<T> >   // 디폴트값 설정
class PriorityQueue{
public:
    void push(const T& data){
        // 1. 우선 힙구조부터 맞춰준다
        _heap.push_back(data);
        // 2. 최말단에서 부모와 도장깨기 시작
        int now = static_cast<int>(_heap.size())-1; // push_back한 data의 index
        while (now > 0) { // 루트 노드까지.
            // 부모노드와 비교해서 더 작으면 패배
            int next = (now-1)/2;
            // if(_heap[now] < _heap[next]) break; 하드코딩된 부분 -> predicate에 따른 기준적용은 아래처럼 이용
            if(_predicate(_heap[now], _heap[next])) break;
            ::swap(_heap[now], _heap[next]);    // 데이터 교체
            now = next;
        }
        
    }

    void pop(){
        // 트리의 구조를 해치지 않기 위해 마지막 데이터를 최상위로 올려줘서 자식과 도장깨기
        _heap[0] = _heap.back();
        _heap.pop_back();
        int now = 0;
        while (true) {
            int left = now*2+1;
            int right = now*2+2;
            if(left >= _heap.size()) break;  // 리프에 도달한 경우, 왼쪽의 유무 경우만 체크해줌.
            int next = now;
            if(_predicate(_heap[next], _heap[left])) next = left; // 왼쪽과 비교
            // 둘 중 승자를 오른쪽과 비교, 오른쪽이 없을 수도 있기에 우선 조건 확인
            if(right < _heap.size() && _predicate(_heap[next], _heap[right])) next = right;

            // 왼오 둘 다 현재 값보다 작으면 종료
            if(next == now) break;

            ::swap(_heap[now], _heap[next]);
            now = next;
        }
    }

    T& top(){
        return _heap[0];
    }

    bool empty(){
        return _heap.empty();
    }
private:
    Container _heap = {};
    Predicate _predicate = {};
};

int main(){
    PriorityQueue<int, vector<int>, greater<int> > pq;
    // less : 내림차순 (defalut) /  greater : 오름차순
    pq.push(100);
    pq.push(500);
    pq.push(300);
    pq.push(400);
    pq.push(200);

    while (!pq.empty()) {
        int value = pq.top();
        pq.pop();
        cout<<value<<endl;
    }
    
    return 0;
}