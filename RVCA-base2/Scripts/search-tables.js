function submitForm(tableName, rowTableId, fields) {
    var filters = getFilters(tableName);

    $.ajax({
        url: '/data/getsearchtablebyfilters',
        data: {
            "fields": fields,
            "filters": filters
        },
        type: 'GET',
        dataType: 'json',
        success: function (ob) {
            InitDataTable(ob, rowTableId);
        }
    });

}

function getFilters(tableName) {
    var filters = {};
    filters.tableName = tableName;
    filters.filtersList = Array();

    var form = document.getElementById("search-form");
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        debugger
        if (e.name == null || e.name == "")
            continue;
        console.log(e.type);
        if (e.value !== "" && (e.type === "text" || e.type === "select-multiple" || e.type === "select-one" || e.type === "radio" && e.checked === true)) {

            var el = {
                columnName: e.name,
                condition: e.value
            };            

            if (e.type === "select-multiple") {
                el.condition = getSelectValues(e);
            }

            if (e.type === 'text') {
                el.condition = '%' + el.condition + '%';
            }

            if (e.name.indexOf('__')>0) {
                debugger
                var realNameAndIndex = e.name.split('__');
                if (realNameAndIndex[1] == 'to')
                    continue;
                el.columnName = realNameAndIndex[0];
                var e2 = document.getElementsByName(el.columnName + "__to")[0];
                el.condition = e.value + '*' + e2.value;
            }

            filters.filtersList.push(el);
        }
    }
    return JSON.stringify(filters);
}

function getSelectValues(select) {
    var result = [];
    var options = select && select.options;
    var opt;

    for (var i = 0, iLen = options.length; i < iLen; i++) {
        opt = options[i];

        if (opt.selected) {
            result.push(opt.value || opt.text);
        }
    }
    return result.join('|')+'|';
}