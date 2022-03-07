var Items = []
function LoadItems(element) {
    if (Items.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/POMaster/getItems',
            success: function (data) {
                Items = data;
                //render catagory
                renderItem(element);
            }
        })
    }
    else {
        //render catagory to the element
        renderItem(element);
    }
}

function renderItem(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    console.log(Items);
    $.each(Items, function (i, val) {
        $ele.append($('<option/>').val(val.value).text(val.text));
    })
}

LoadItems($('#Icode'));


var GSTItems = []
function LoadGSTItems(element) {
    if (GSTItems.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/POMaster/getGSTItems',
            success: function (data) {
                GSTItems = data;
                //render catagory
                renderGSTItem(element);
            }
        })
    }
    else {
        //render catagory to the element
        renderGSTItem(element);
    }
}

function renderGSTItem(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    console.log(GSTItems);
    $.each(GSTItems, function (i, val) {
        $ele.append($('<option/>').val(val.value).text(val.text));
    })
}

LoadGSTItems($('#ItemGST'));


var UnitItems = []
function LoadUnitItems(element) {
    if (UnitItems.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/POMaster/getUnitItems',
            success: function (data) {
                UnitItems = data;
                //render catagory
                renderUnitItem(element);
            }
        })
    }
    else {
        //render catagory to the element
        renderUnitItem(element);
    }
}

function renderUnitItem(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    console.log(UnitItems);
    $.each(UnitItems, function (i, val) {
        $ele.append($('<option/>').val(val.value).text(val.text));
    })
}

LoadUnitItems($('#RateUnit'));



//fetch products
function LoadUnit(ItemDD) {
    $.ajax({
        type: "GET",
        url: "/POMaster/getItemDetails",
        data: { 'id': $(ItemDD).val() },
        success: function (data) {
            cons
            renderHSN($(ItemDD).parents('.detailcontainer').find('select.ItemGST'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function renderHSN(element, data) {
    //render product
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.value).text(val.text));
    })
}