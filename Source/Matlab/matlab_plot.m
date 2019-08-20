% 
% Matlab code by Andrew Kosmachev
% mailto: avkos1985@gmail.com
% St.Petersburg, Russia
% 09/2019
%

function [] = matlab_plot(  )
%% Draw plot at WPF window
    
    hfig = figure(1);    % create figure and save handle
    set(hfig,'DeleteFcn',@fig_close_callback) % setcallback to close event

    % Paste your plot code here    
    x = -pi:0.01:pi;
    y = sin(x); 
    plot(x,y)
    %
    
    drawnow     % initiative draw

    t = timer('StartDelay', 0.5, 'Period', 0.5, 'TasksToExecute', Inf, ...
          'ExecutionMode', 'fixedRate'); % create timer (you may decrease 'Period' value to increase plot updating rate)

    t.TimerFcn = { @timer_callback, 'timer_callback'}; % set callback to timer event
    start(t) % start timer
    
    global global_t;
    global_t = t;   % save timer handle to global
end

function [] = timer_callback(obj, event, text_arg)
%% Timer callback
    %'timer_callback'
    drawnow % initiative draw (during every 0.5 sec)
end

function [] = fig_close_callback(src,callbackdata)
%% Close figure event
    %'fig_close_callback'
    global global_t; 
    t = global_t;   % load timer handle from global
    stop(t);        % stop timer
    global_t = [];    % delete timer handle in global
end