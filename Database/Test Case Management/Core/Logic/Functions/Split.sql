ALTER FUNCTION Split
 (@List  varchar(1000))

RETURNS @Results table
 (Item varchar(1000))

AS

begin
 declare @IndexStart int
 declare @IndexEnd int
 declare @Length  int
 declare @Delim  char(1)
 declare @Word  varchar(1000)

 set @IndexStart = 1 
 set @IndexEnd = 0

 set @Length = len(@List) 
 set @Delim = ','
 
 while @IndexStart <= @Length
      begin
  
  set @IndexEnd = charindex(@Delim, @List, @IndexStart)
  
  if @IndexEnd = 0
   set @IndexEnd = @Length + 1
  
  set @Word = substring(@List, @IndexStart, @IndexEnd - @IndexStart)
  
  set @IndexStart = @IndexEnd + 1
  
  INSERT INTO @Results
   SELECT @Word
      end
 
 return
end
GO

