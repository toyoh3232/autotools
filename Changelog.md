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
- [x] 主スレッドがバックグラウンドスレッドを終わらせる方法を再考、
      Abortはbad design. volatile を使用
- [x] サブスレッドtry catch最適化

##### 20.05.14（木）
- [ ] 異常、エラー処理と表示を統一化 