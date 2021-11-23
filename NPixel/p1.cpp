#include <iostream>
#include <vector>
#include <cstdlib>
using namespace std;

// 1. vector<int> 를 반환하고 입력은 vector<int>, int n, int d입니다.
// 벡터 안에서 특정 조건에 모두 부합하는 데이터를 삭제하고 남은 데이터를 출력하는 문제
// 조건 1. 처음과 끝에 있는 데이터는 삭제하지 않는다.
// 조건 2. 현재 데이터와 다음 데이터 간의 절댓값 차이가 n이하일 경우 삭제합니다.
// 조건 3. 연속으로 데이터를 삭제할 경우 d 이하의 횟수일 경우 삭제합니다.

// ex1) 4, 3, 7, -3, 6, -2, -1, 8, -3, 0

vector<int> solution(vector<int> data, int n, int d){
    vector<int>::iterator start = data.begin()+1;
    int deleteCount = 0;
    for (vector<int>::iterator iter = start; iter != data.end()-1 ; ++iter)
    {
        int num = abs(*iter-*(iter+1));  // 현재 데이터와 다음데이터의 절댓값 차이
        if(deleteCount<d){
            if(num <= n) {
                data.erase(iter);
                --iter;
                ++deleteCount;
            }
            else deleteCount=0;
        } 
        else deleteCount=0;
    }
    for (int i = 0; i < data.size(); ++i){
        cout<<data[i]<<endl;
    }
    return data;
}

int main(){
    vector<int> data;
    data.push_back(4);
    data.push_back(3);
    data.push_back(7);
    data.push_back(-3);
    data.push_back(6);
    data.push_back(-2);
    data.push_back(-1);
    data.push_back(8);
    data.push_back(-3);
    data.push_back(0);
    solution(data, 4, 2);
    
    return 0;
}