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
