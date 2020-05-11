##### 5.08（金）
* Optionsクラスの構想

##### 5.09（土）
* LINQの利用より引数解析を実装

##### 5.10（日）
* JsonSerializeはNet.Coreの機能のため、断念、XMLを採用
* LoadとSaveのタイミングを決定
* Databindingの可能性模索？                  

##### 5.11（月）
* Optionsのメンバー変数とControlsの処理を一致(nullable typeの活用)
* OptionsのCurrent Dir Indexを追加(AddSourceDir/AddTargetDir)
* Data race conditionを防ぐため、Thread parameterを再設定			
* Directory Path文字列OSと統一化			
* SMB pathサポート追加						

##### 5.12（火）
* エラー処理とMessageBoxの表示（Modalにする）
* 主スレッドがバックグラウンドスレッドを終わらせる方法を再考、
  Abortはbad design. voilate を使用


