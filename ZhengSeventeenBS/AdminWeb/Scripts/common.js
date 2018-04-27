/**
 * 将带有关键字的数组转化成树
 * @param {Object} data 原数组
 * @param {Object} id   item主键
 * @param {Object} pid  item父标识
 */
function toTree(data, id, pid) {
    // 删除 所有 children,以防止多次调用
    data.forEach(function (item) {
        delete item.child;
    });
    // 将数据存储为 以 id 为 KEY 的 map 索引数据列
    var map = {};
    data.forEach(function (item) {
        map[item[id]] = item;
    });
    var val = [];
    data.forEach(function (item) {
        // 以当前遍历项，的pid,去map对象中找到索引的id
        var parent = map[item[pid]];
        // 好绕啊，如果找到索引，那么说明此项不在顶级当中,那么需要把此项添加到，他对应的父级中
        if (parent) {
            (parent.child || (parent.child = [])).push(item);
        } else {
            //如果没有在map中找到对应的索引ID,那么直接把 当前的item添加到 val结果集中，作为顶级
            val.push(item);
        }
    });
    return val;
}
var dataTablesLanguage =
    { //国际化配置  
        "sProcessing": "正在获取数据，请稍后...",
        "sLengthMenu": "每页 _MENU_ 条",
        "sZeroRecords": "没有您要搜索的内容",
        "sInfo": "_START_ 到  _END_ 条 共 _TOTAL_ 条",
        "sInfoEmpty": "",
        "sInfoFiltered": "",
        "sInfoPostFix": "",
        "sSearch": "<span class='l ml - 5 pt-2'>搜索：</span >",
        "sUrl": "",
        "oPaginate": {
            "sFirst": "首页",
            "sPrevious": "上一页",
            "sNext": "下一页",
            "sLast": "末页"
        }
    }
//树类数组排序
function treeArrSort(nodeArr, id, pid) {
    var treeArr = [];
    var node = toTree(nodeArr, id, pid);
    for (var i = 0; i < node.length; i++) { treeArr = traverseTree(node[i], treeArr) };
    return treeArr;
}
function traverseTree(node, treeArr) {
    if (!node) {
        return treeArr;
    }
    var nodeChild = node.child;
    treeArr = traverseNode(node, treeArr);
    if (nodeChild && nodeChild.length > 0) {
        var i = 0;
        for (i = 0; i < nodeChild.length; i++) {
            treeArr = this.traverseTree(nodeChild[i], treeArr);
        }
    }
    return treeArr
}
function traverseNode(node, treeArr) {
    delete node.child;
    treeArr.push(node);
    return treeArr;
}
//树类数组排序结束