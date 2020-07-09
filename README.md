# Spider
HtmlAgilityPack
sqlbulkcopy
需要分析网页页面布局 
利用x-path 对需要的内容进行抓取保存
有时会出现复制xpath抓取内容为空的问题 可能是因为浏览器复制的xpath会自动优化，自己加上tbody，其实网页源代码里是没有的。去掉xpath 即可
当截取的htmlNode 中 没有table标签 存在tr  td时 xpath可能找不到目标 可以江tr td 替换为ul  li  只有td 时 可将td 替换成单独的其他标签 p等
