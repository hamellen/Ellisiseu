using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TokenManager 
{
    public void RefreshToken(ref CancellationTokenSource cts)
    {

        if (cts != null)
        {

            if (!cts.IsCancellationRequested)
            { //취소 요청이 없을시 
                cts.Cancel();
            }
            cts.Dispose();
        }

        cts = new CancellationTokenSource();//새로운 토큰 생성
    }
}
