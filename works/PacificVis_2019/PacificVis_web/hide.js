let info = $('#Layer_2')
// info.css({'display':'none'})
let isDisplay = false;
let circle = $('.cls-6')

circle.on('mouseover', (e) => {
    if(isDisplay === false){
        $('#Layer_2').fadeIn('1000')
        isDisplay = !isDisplay
    }else{
        $('#Layer_2').fadeOut('1000')
        isDisplay = !isDisplay
    }

})

