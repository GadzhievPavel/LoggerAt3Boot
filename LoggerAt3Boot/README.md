# LoggerAt3boot
# Пример
<pre>
<code>
    var logger = new LoggerAt3Boot.LoggerAt3Boot("D:\\temp");
    logger.setLimitSize(1024*1024);
    logger.error("text");
    logger.warning("text");
    logger.info("text");
</code>
</pre>

- setLimitSize - задаем максимальный размер файла с логом
