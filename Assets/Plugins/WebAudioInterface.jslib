mergeInto(LibraryManager.library, {

  StartRecording: function(){
    interface_StartRecording();
  },

    StopRecording: function(){
    interface_StopRecording();
  },

    StartRecordingPlayback: function(){
    interface_StartRecordingPlayback();
  },
  
    StopRecordingPlayback: function(){
    interface_StopRecordingPlayback();
  },

  SetFileNameAndSend: function(str){
    interface_SetFileName(Pointer_stringify(str));
  },




});