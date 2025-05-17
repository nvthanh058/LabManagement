function getWindowSize() {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};

function printComponent(componentSelector) {   
    var elementToPrint = document.querySelector(componentSelector);
    if (elementToPrint) {

        var printWindow = window.open('', '_blank');
        printWindow.document.open();
        var html = '<html>';        
        html += '<head>';
        html += '<meta charset="utf-8" />';
        html += '<title>Printing</title>';
        html += '<link href="css/bootstrap.min.css" type="text/css" />';
        html += '<link href="css/app.min.css" type="text/css" />';
        html += '<style>';
        html += ' body {';
        html += '   zoom: 80%;';
        html += ' }';
        html += ' .breakhere {';
        html += '   display: block;';
        html += '   clear: both;';
        html += '   page-break-after: always;';
        html += ' }';
        html += ' .page-content .page-size{';
        html += '   height: 80vh;';
        html += '   margin: 5px;';
        html += ' }';
        html += '.item {';
        html += '   margin - top: 5px;';
        html += ' }';
        html += ' </style>';
        html += '  </head>';
        html += '  <body >';
        html += '    <div class="main-content">';
        html += '       <div class="page-content">';        
        html += elementToPrint.innerHTML;
        html += '       </div>';
        html += '    </div>';
        html += '  </body>';
        html += '</html>';
        
        printWindow.document.write(html);        
        printWindow.document.close();
        printWindow.print();
        printWindow.close();
    }
}


urlFileDownload = (fileName, url) => {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}

window.saveAsFile = (filename, content) => {
    const link = document.createElement('a');
    link.download = filename;
    link.href = 'data:application/octet-stream;base64,' + content;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};


function changeRadius() {  
    document.getElementById("teeth_01").setAttribute("style", "fill:blue;stroke:#000000;stroke-width:10;stroke-linecap:butt;stroke-linejoin:round;stroke-miterlimit:4;stroke-opacity:1;stroke-dasharray:none");
}