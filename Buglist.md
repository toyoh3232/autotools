## Bugs List

***
1.  **問　題**：参照ボタンからフォルダを選択したら、「Itemへ操作無効」との異常発生
    
    **原　因**：Serializationを考える前までは、DataBindingでなく直接各Controlの属性を変更するという手法だった。

    **解決策**：Textへの反映することにした。DataBindingSourceの更新は、Startボタンがやる。

***
2.  **問　題**：Threadが開始後、異常発生した場合でも、試験時間タイマーが動作継続。
    
    **原　因**：試験時間タイマーを止めなかった。

    **解決策**：try catchの中に主Threadの試験時間タイマーを制御する。

***
3.  **問　題**：大きなファイルがある場合、Startボタン連続に押したら、「閉じているファイルにはアクセスできません」との異常発生
    
    **原　因**：容量を示すStatusの計算は、BeginInvodeでありながらDisposable objectsを主Threadに渡したため。
    
    ```csharp
    this.BeginInvoke(new Action(() => UpdateAppStatus("copied:",
                                        string.Format("{0:d}M / {1:d}M", bytes / 1024 / 1024, readStream.Length / 1024 / 1024),
                                        false)));
    ```

    **解決策**：temp変数を使用
    ```csharp
    var length = readStream.Length;
    this.BeginInvoke(new Action(() => UpdateAppStatus("copied:",
                                        string.Format("{0:d}M / {1:d}M", bytes / 1024 / 1024, length / 1024 / 1024),
                                        false)));
    ```

***
4.  **問　題**：Stopボタン押しても、容量Statusのメッセージが残っている。
    
    **原　因**：容量メッセージは消えない仕組みにしている。

    **解決策**：Statusの表示方法など統一してから、今後対処。
	
***
5.  **問　題**：「回数を制限する」CheckBoxを解除しても、「自動シャットダウン」Checkbox解除されない。
    
    **原　因**：上記2つのCheckBoxの相互制御関係が不明瞭

    **解決策**：制御コード追加
***
6.  **問　題**：Stopボタンをしたら、Freeze発生
    
    **原　因**：volatile 手法を採用したが、Sub ThreadがBlockなInvokeを使っている。WaitStopSleep状態になる

    **解決策**：Sub ThreadがInvokeを使う必要な場合があるから、Abort手法に戻す。
***
7.  **問　題**: 作業開始しても、Optionsの設定が有効になっている。
    
    **原　因**：Controlの制御し忘れ
    
    **解決策**：UpdateControl関数を修正

8.  **問　題**: コマンドラインで存在しないフォルダーを与えたら、ComboBoxに残る
    
    **原　因**：コマンドラインがある場合とない場合、区別していない。
    
    **解決策**：コマンドラインがある場合はシリアル化せずに、1回きりの処理にする。コマンドラインに不備がある場合は、使用法提示で終わり、ない場合は、
ComboBoxに追加されるが、シリアル化はされない。有効なフォルダの判断はButton側がやるので、GUIの時だけシリアル化。



