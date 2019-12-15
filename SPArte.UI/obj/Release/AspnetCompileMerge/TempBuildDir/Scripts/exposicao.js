
$(document).ready(function () {

    //Informaçoes da exposição
    $.ajax(
        {
            url: url_exposicao_info, type: "GET", dataType: 'json', cache: false, async: true
        })
        .done(function (json) { 
            $('#info').text(json.data[0].informacoes);         
          
        })
        .fail(function () {
            console.log("Falha na requisiçao");
        });
    //Obras expostas
    $.ajax(
        {
            url: url_expostas, type: "GET", dataType: 'json', cache: false, async: true
        })
        .done(function (json) {
            var obras_data = '';    

            console.log(Object.keys(json.data).length);
            if (Object.keys(json.data).length <= 1) {

                $('#obras').append(' <p>Nenhuma obra na exposição</p>');  
                $("#remove").hide();
                return;
            };

            $.each(json.data, function (i, item) {
                if (item.capa == "S")
                {
                    $("#imgcapa").attr("src", item.url);
                };            
                console.log(i+1);
                obras_data += '<div class="col-6 col-md-4 archive-item" index="'+(i+1)+'">';                
                obras_data += '<div class="archive-item-image">';
                obras_data += '<img src="' + item.url +'"width="200" height="250">';
                obras_data += '</div>';
                obras_data += '<div class="archive-item-texts">';
                obras_data += '<div class="favorite-icon"><i class="icon-heart"></i></div>';
                obras_data += '<p><b>' + item.nome + '</b><br><b>'+item.autor+'</b></p>';              
                obras_data += '</div>';               
                obras_data += '</div>';
     
            });       
            $('#obras').append(obras_data);
            $("#remove").show();
          
        })
        .fail(function () {
            console.log("Falha na requisiçao");
        });  
});