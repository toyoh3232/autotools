##### 20.05.08（金）
- [x] Optionsクラスの構想

##### 20.05.09（土）
- [x] LINQの利用より引数解析を実装

##### 20.05.10（日）
- [x] JsonSerializeはNet.Coreの機能のため、断念、XMLを採用
- [x] LoadとSaveのタイミングを決定
- [x] Databindingの可能性模索？                  

##### 20.05.11（月）
- [x] Optionsのメンバー変数とControlsの処理を一致(nullable typeの活用)
- [x] OptionsのCurrent Dir Indexを追加(AddSourceDir/AddTargetDir)
- [x] Data race conditionを防ぐため、Thread parameterを再設定
- [x] Directory Path文字列OSと統一化
- [x] SMB pathサポート追加	

##### 20.05.12（火）
- [x] Bug listの編集

##### 20.05.13（水）
- [x] エラー処理とMessageBoxの表示（Modalにする）のちに別ThreadでShowMessageBoxを実装したためと判明
- [x] ~~主スレッドがバックグラウンドスレッドを終わらせる方法を再考、
      Abortはbad design. volatile を使用~~
- [x] サブスレッドtry catch最適化

##### 20.05.14（木）
- [x] 異常、エラー処理と表示を統一化 

##### 20.05.26（火）
- [x] Bug 7を修正
- [x] CommandLineのHelpボタンを追加
- [x] ひとつの実行ファイルにまとめ

##### 20.05.28（木）
- [x] Bug 8対応、CommandLineで起動された（引数あり）場合、serialization化しない
- [x] 引数に不備がある場合、使用法提示、Exit 
- [x] Working スレッドの停止中にあたる待ち時間処理（もう1つのスレッドで処理）
- [x] Startボタンロジカル調整
- [x] 翻訳修正
- [x] ディレクトリが存在しない場合のエラーメッセージを分別化
- [x] 試験時間タイマーを停止するのをスレッドが終わる前にする

##### 20.06.05（金）V1.03
- [x] Bug 9対応、ついでに例外入力処理
- [x] CounterをLambdaのClosure化
- [ ] ~~前回試験開始時間、経過時間と区別~~

##### 20.06.08（月）V1.04
- [x] 前回試験開始時間、経過時間と区別
- [x] Thread正常終了と異常終了を区別し、画面表示細分化（UpdateControl）
- [x] Version情報をReflectionにより取得
- [x] 翻訳修正
 
##### 20.06.16（火）V1.05
- [x] Bug 10対応
- [x] ComandLineヘルプと翻訳修正

