[v]新增Activity Index的VM
[v]新增Activity Index 的 Exts (entity 轉 ActivityIndexVM)
[v]新增ActivityController、新增 Activity的Index Action
[v]新增 【Index,Activity】的Layout連結

***Activity的增刪查改：參考Allen 的 202306.solution 【商品】

[Working On] Activity 的 Create
	[v]新增Activity Create的VM
	[v ]新增Activity Create 的 Exts (entity 轉 ActivityCreateVM)
	[v]新增ActivityController、新增 Activity 的 Create Action[Http]
	[v]新增ActivityController、新增 Activity 的 Create Action[Post]
	[v]新增Activity Create的檢視

[v] VM加上顯示中文 [Display(Name = "商品分類")]
[v ] Index的VM不用欄位驗證
[] Description太長的話，字數截斷
[v]寫Activity Create 的 Exts (vm 轉 entity)

--6/29 21:00 Activity 的  Create 照片新增的部分失敗 !
			 Allen錄影檔看到20230609 14:58

***【Activity 的 Create完成，但新增照片有問題，待修正】***
			

[ Working On ]Activity 的 Edit
	[v]新增Activity Edit的VM
	[v]新增Activity Edit 的 Exts (entity 轉 ActivityEditVM)
	[v]新增Activity Edit 的 Exts (ActivityEditVM 轉 entity )
	[v]新增ActivityController、新增 Activity 的 Edit Action[Http]
	
	

	Allen錄影檔看到20230609 15:16

	--6/30 18:24 Activity 的  Edit 編輯的部分無法順利編輯，已經填的表格還會跑出驗證訊息

*測試家裡電腦的git

***【Activity的Edit完成，但更新日期會不能寫時間，照片編輯無法成功，待修正】***
	[待修正]新增Activity Edit的檢視
	[待修正]新增ActivityController、新增 Activity 的 Edit Action[Post]


[ v ]Activity 的 Search

[ v ] 查詢功能 影片檔案20230609 15:17
[ v ] 把ViewBag的List寫成副程式，讓Action可以叫用它


***【Activity的搜尋功能沒辦法記住歷史查詢過的字，待修正】***
[ v ]Activity的搜尋完成，修正好記住歷史資訊的部份

	Allen錄影檔看到20230609 整天結束