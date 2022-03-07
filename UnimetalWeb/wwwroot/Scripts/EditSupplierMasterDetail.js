

$(document).ready(function () {

    //Add button click event
    $('#add').click(function () {
        //validation and add order items
        var isAllValid = true;
        if (!$('#ContactType').val()) {
            isAllValid = false;
            $('#ContactType').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#ContactType').siblings('span.error').css('visibility', 'hidden');
        }
        if (!$('#txtName').val()) {
            isAllValid = false;
            $('#txtName').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#txtName').siblings('span.error').css('visibility', 'hidden');
        }
        if (!$('#Designation').val()) {
            isAllValid = false;
            $('#Designation').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Designation').siblings('span.error').css('visibility', 'hidden');
        }



        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#ContactType,#txtName,#Desination,#ContactNo,#EmailId,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#tblsuppContactDetails').append($newRow);

            //clear select data

            $('#ContactType,#txtName,#Desination,#ContactNo,#EmailId').val('');
            $('#ErrorSupplierContactDetails').empty();
        }

    })


    //Add button click event
    $('#SupplierQualityDetailmainrowadd').click(function () {
        //validation and add order items
        var isAllValid = true;
        if (!$('#InstrumentName').val()) {
            isAllValid = false;
            $('#InstrumentName').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#InstrumentName').siblings('span.error').css('visibility', 'hidden');
        }
        if (!$('#Make1').val()) {
            isAllValid = false;
            $('#Make1').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Make1').siblings('span.error').css('visibility', 'hidden');
        }
        if (!$('#Qty').val()) {
            isAllValid = false;
            $('#Qty').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Qty').siblings('span.error').css('visibility', 'hidden');
        }



        if (isAllValid) {
            var $newRow = $('#SupplierQualityDetailmainrow').clone().removeAttr('id');

            //Replace add button with remove button
            $('#SupplierQualityDetailmainrowadd', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#InstrumentName,#Make1,#Qty,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#tblrequestSupplierQualityDetail').append($newRow);

            //clear select data

            $('#InstrumentName,#Make1,#Qty').val('');
            $('#ErrorrequestSupplierQualityDetail').empty();
        }

    })


    $('#Machineryadd').click(function () {
        //validation and add order items
        var isAllValid = true;
        if (!$('#MachineName').val()) {
            isAllValid = false;
            $('#MachineName').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#MachineName').siblings('span.error').css('visibility', 'hidden');
        }
        if (!$('#Make').val()) {
            isAllValid = false;
            $('#Make').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Make').siblings('span.error').css('visibility', 'hidden');
        }
        if (!$('#Capacity').val()) {
            isAllValid = false;
            $('#Capacity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Capacity').siblings('span.error').css('visibility', 'hidden');
        }
        if (!$('#MQty').val()) {
            isAllValid = false;
            $('#MQty').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#MQty').siblings('span.error').css('visibility', 'hidden');
        }




        if (isAllValid) {
            var $newRow = $('#SupplierMachineryDetailsrow').clone().removeAttr('id');

            //Replace add button with remove button
            $('#Machineryadd', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#MachineName,#Make,#Capacity,#MQty,#Machineryadd', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#tblSupplierMachineryDetails').append($newRow);

            //clear select data

            $('#MachineName,#Make,#Capacity,#MQty,#Machineryadd').val('');
            $('#Machineryadd').val('add');

            $('#ErrortblSupplierMachineryDetails').empty();
        }

    })

    //remove button click event
    $('#tblSupplierMachineryDetails').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    //remove button click event
    $('#tblsuppContactDetails').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    //remove button click event
    $('#tblSupplierMachineryDetails').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#ErrorSupplierContactDetails').text('');
        $('#ErrortblSupplierMachineryDetails').text('');
        var list = [];
        var requestSupplierMachineryDetailslist = [];
        var requestSupplierQualityDetaillist = [];
        var errorItemCount = 0;

        $('#tblsuppContactDetails tbody tr').each(function (index, ele) {
            console.log(index, ele);
            console.log($('.ContactType', this).val());

            var requestSupplierContactDetail = {

                ContactType: $('.ContactType', this).val(),
                Name: $('.txtName', this).val(),
                Designation: $('.Designation', this).val(),
                ContactNo: $('.ContactNo', this).val(),
                EmailId: $('.EmailId', this).val(),
            }
            list.push(requestSupplierContactDetail);

        })

        $('#tblrequestSupplierQualityDetail tbody tr').each(function (index, ele) {
            console.log(index, ele);
            console.log($('.MachineName', this).val());

            var requestSupplierQualityDetail = {

                InstrumentName: $('.InstrumentName', this).val(),
                Make: $('.Make1', this).val(),
                CalibrationFacility: $('.CalibrationFacility', this).prop('checked'),
                Qty: $('.Qty', this).val()
            }
            requestSupplierQualityDetaillist.push(requestSupplierQualityDetail);

        })
        $('#tblSupplierMachineryDetails tbody tr').each(function (index, ele) {
            console.log(index, ele);
            console.log($('.MachineName', this).val());

            var requestSupplierMachineryDetails = {

                MachineName: $('.MachineName', this).val(),
                Make: $('.Make', this).val(),
                Capacity: $('.Capacity', this).val(),
                Qty: $('.MQty', this).val()
            }
            requestSupplierMachineryDetailslist.push(requestSupplierMachineryDetails);

        })

        if (errorItemCount > 0) {
            $('#ErrorSupplierContactDetails').text(errorItemCount + " invalid entry in order item list.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#ErrorSupplierContactDetails').text('At least 1  item required.');
            isAllValid = false;
        }
        if (requestSupplierMachineryDetailslist.length == 0) {
            $('#ErrortblSupplierMachineryDetails').text('At least 1  item required.');
            isAllValid = false;
        }
        if (requestSupplierQualityDetaillist.length == 0) {
            $('#ErrorrequestSupplierQualityDetail').text('At least 1  item required.');
            isAllValid = false;
        }

        if ($('#Name').val().trim() == '') {
            $('#Name').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#Name').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#Address').val().trim() == '') {
            $('#Address').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#Address').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var data = {
                Name: $('#Name').val().trim(),
                Address: $('#Address').val().trim(),
                PhoneNo: $('#PhoneNo').val().trim(),
                YearofCommencement: $('#YearofCommencement').val().trim(),

                PhoneNo: $('#PhoneNo').val().trim(),
                GSTNo: $('#GSTNo').val().trim(),
                PAN: $('#PAN').val().trim(),
                TAN: $('#TAN').val().trim(),
                CIN: $('#CIN').val().trim(),
                UdyamRegistrationCertificateNo: $('#UdyamRegistrationCertificateNo').val().trim(),
                Category: $('#Category').val().trim(),
                Constitution: $('#Constitution').val().trim(),
                WeeklyOff: $('#WeeklyOff').val().trim(),
                Workinghours: $('#Workinghours').val().trim(),
                DetailsofProductsorServices: $('#DetailsofProductsorServices').val().trim(),
                LANDArea: $('#LANDArea').val().trim(),
                BUILDINGArea: $('#BUILDINGArea').val().trim(),
                Bankers: $('#Bankers').val().trim(),
                Certification: $('#Certification').val().trim(),
                TransportationDacility: $('#TransportationDacility').val().trim(),
                TurnoverofLastTwoYears: $('#TurnoverofLastTwoYears').val().trim(),
                OverallProductionCapacity: $('#OverallProductionCapacity').val().trim(),
                CurrentCapacityUtilize: $('#CurrentCapacityUtilize').val().trim(),
                SpareCapacity: $('#SpareCapacity').val().trim(),
                NoofSamples: $('#NoofSamples').val().trim(),
                MsmeCertificate: $('#MsmeCertificate').val().trim(),
                TallyName: $('#TallyName').val().trim(),
                SafetyOrEnvironmentalCompliance: $('#SafetyOrEnvironmentalCompliance').prop('checked'),
                ProductsOrProcessEnvironmentFriendly: $('#ProductsOrProcessEnvironmentFriendly').prop('checked'),
                HazardousWasteManagementProcess: $('#HazardousWasteManagementProcess').prop('checked'),
                ReachAndRoHSCompliance: $('#ReachAndRoHSCompliance').prop('checked'),
                MsmeApplicable: $('#MsmeApplicable').prop('checked'),
                requestSupplierContactDetail: list,
                requestSupplierMachineryDetail: requestSupplierMachineryDetailslist,
                requestSupplierQualityDetail: requestSupplierQualityDetaillist
            }

            $(this).val('Please wait...');

            $.ajax({
                type: 'POST',
                url: '/SupplierMaster/Edit',
                data: data,
                contentType: 'application/x-www-form-urlencoded',
                success: function (data) {
                    if (data.status) {
                        window.location.href = redirecturl;
                        alert('Successfully saved');
                        //here we will clear the form
                        list = [];
                        $('#Name,#Address,#YearofCommencement').val('');
                        $('#tblsuppContactDetails').empty();
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').val('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').val('Save');
                }
            });
        }

    });

});

